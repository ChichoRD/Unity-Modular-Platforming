using System.Collections;
using UnityEngine;

public class CoyoteTimeConstraint : MonoBehaviour, IMovementConstraint
{
    [RequireInterface(typeof(IMovementConstraint))]
    [SerializeField]
    private Object _movementConstraintObject;
    private IMovementConstraint MovementConstraint => _movementConstraintObject as IMovementConstraint;

    [RequireInterface(typeof(IObservableJumpController))]
    [SerializeField]
    private Object _observableJumpControllerObject;
    private IObservableJumpController ObservableJumpController => _observableJumpControllerObject as IObservableJumpController;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _durationProviderObject;
    private IDurationProvider DurationProvider => _durationProviderObject as IDurationProvider;

    private Coroutine _coyoteTimeCoroutine;

    private void Awake()
    {
        ObservableJumpController.DescentStarted.AddListener(OnDescentStarted);
    }

    private void OnDestroy()
    {
        ObservableJumpController.DescentStarted.RemoveListener(OnDescentStarted);
    }

    public bool CanPerformMovement() => MovementConstraint.CanPerformMovement() || TryStopCoyoteTime();

    private bool TryStartCoyoteTime()
    {
        if (_coyoteTimeCoroutine != null) return false;
        _coyoteTimeCoroutine = StartCoroutine(CoyoteTimeCoroutine());
        return true;
    }

    private bool TryStopCoyoteTime()
    {
        if (_coyoteTimeCoroutine == null) return false;
        StopCoroutine(_coyoteTimeCoroutine);
        _coyoteTimeCoroutine = null;
        return true;
    }

    private IEnumerator CoyoteTimeCoroutine()
    {
        yield return new WaitForSeconds((float)(DurationProvider?.GetDuration().TotalSeconds ?? 0.0f));
        _coyoteTimeCoroutine = null;
    }

    private void OnDescentStarted()
    {
        _ = MovementConstraint.CanPerformMovement()
            && (TryStartCoyoteTime()
                || (TryStopCoyoteTime()
                    && TryStartCoyoteTime()));
    }
}