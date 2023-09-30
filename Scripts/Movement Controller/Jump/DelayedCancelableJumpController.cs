using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class DelayedCancelableJumpController : MonoBehaviour, IJumpController
{
    [RequireInterface(typeof(IJumpController))]
    [SerializeField]
    private Object _jumpControllerObject;
    private IJumpController JumpController => _jumpControllerObject as IJumpController;

    [RequireInterface(typeof(IObservableJumpController))]
    [SerializeField]
    private Object _observableJumpControllerObject;
    private IObservableJumpController ObservableJumpController => _observableJumpControllerObject as IObservableJumpController;

    [RequireInterface(typeof(IDurationProvider))] // TODO - Parametrize a profile in terms of minimum jump height
    [SerializeField]
    private Object _jumpCancelMinimumDelayProviderObject;
    private IDurationProvider JumpCancelMinimumDelayProvider => _jumpCancelMinimumDelayProviderObject as IDurationProvider;

    private Coroutine _jumpCancelWaitCoroutine;
    private bool CanCancelJump => _jumpCancelWaitCoroutine == null;

    private void Awake()
    {
        ObservableJumpController.AscentStarted.AddListener(OnAscentStarted);
    }

    private void OnDestroy()
    {
        ObservableJumpController.AscentStarted.AddListener(OnAscentStarted);
    }

    public IMovementPerformer GetGravityPerformer() => JumpController.GetGravityPerformer();
    public IMovementPerformer GetImpulsePerformer() => JumpController.GetImpulsePerformer();
    public IMovementPerformer GetCancelerPerformer() => CanCancelJump
                                                        ? JumpController.GetCancelerPerformer()
                                                        : null;

    private void OnAscentStarted()
    {
        TryStopJumpCancelWaitCoroutine();
        _jumpCancelWaitCoroutine = StartCoroutine(JumpCancelWaitCoroutine());
    }

    private bool TryStopJumpCancelWaitCoroutine()
    {
        if (_jumpCancelWaitCoroutine == null) return false;

        StopCoroutine(_jumpCancelWaitCoroutine);
        _jumpCancelWaitCoroutine = null;
        return true;
    }

    private IEnumerator JumpCancelWaitCoroutine()
    {
        yield return new WaitForSeconds((float)JumpCancelMinimumDelayProvider.GetDuration().TotalSeconds);
        _jumpCancelWaitCoroutine = null;
    }
}