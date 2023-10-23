using System.Collections;
using UnityEngine;

public class BufferedJumpImpulsePerformer : MonoBehaviour, IMovementPerformer
{
    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _movementPerformerObject;
    private IMovementPerformer MovementPerformer => _movementPerformerObject as IMovementPerformer;

    [RequireInterface(typeof(IObservableJumpController))]
    [SerializeField]
    private Object _observableJumpControllerObject;
    private IObservableJumpController ObservableJumpController => _observableJumpControllerObject as IObservableJumpController;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _durationProviderObject;
    private IDurationProvider DurationProvider => _durationProviderObject as IDurationProvider;

    private Coroutine _performedLastMovementCoroutine;
    private bool _performedLastMovement;

    private void Awake()
    {
        ObservableJumpController.DescentEnded.AddListener(OnDescentEnded);
    }

    private void OnDestroy()
    {
        ObservableJumpController.DescentEnded.RemoveListener(OnDescentEnded);
    }

    public bool TryPerformMovement() => (_performedLastMovement = MovementPerformer.TryPerformMovement())
                                        & (TryStartPerformedLastMovementCoroutine()
                                           || (TryStopPerformedLastMovementCoroutine()
                                               && TryStartPerformedLastMovementCoroutine()));

    private void OnDescentEnded()
    {
        _ = _performedLastMovement
            || !TryStopPerformedLastMovementCoroutine()
            || TryPerformMovement();
    }

    private bool TryStartPerformedLastMovementCoroutine()
    {
        if (_performedLastMovementCoroutine != null) return false;
        _performedLastMovementCoroutine = StartCoroutine(PerformedLastMovementCoroutine());
        return true;
    }

    private bool TryStopPerformedLastMovementCoroutine()
    {
        if (_performedLastMovementCoroutine == null) return false;
        StopCoroutine(_performedLastMovementCoroutine);
        _performedLastMovementCoroutine = null;
        return true;
    }

    private IEnumerator PerformedLastMovementCoroutine()
    {
        yield return new WaitForSeconds((float)(DurationProvider?.GetDuration().TotalSeconds ?? 0.0f));
        _performedLastMovementCoroutine = null;
    }
}