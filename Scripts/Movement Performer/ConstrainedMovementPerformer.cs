using UnityEngine;

public class ConstrainedMovementPerformer : MonoBehaviour, IMovementPerformer
{
    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _movementPerformerObject;
    private IMovementPerformer MovementPerformer => _movementPerformerObject as IMovementPerformer;

    [RequireInterface(typeof(IMovementConstraint))]
    [SerializeField]
    private Object _movementConstraintObject;
    private IMovementConstraint MovementConstraint => _movementConstraintObject as IMovementConstraint;

    [SerializeField] private bool _allowNullConstraint = true;

    public bool TryPerformMovement(IRigidbodyAccessor rigidbodyAccessor) => (MovementConstraint?.CanPerformMovement() ?? _allowNullConstraint)
                                                                            && MovementPerformer != null
                                                                            && MovementPerformer.TryPerformMovement(rigidbodyAccessor);
}
