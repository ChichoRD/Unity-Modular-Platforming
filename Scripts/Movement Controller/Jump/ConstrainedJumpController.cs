using UnityEngine;
using Object = UnityEngine.Object;

public class ConstrainedJumpController : MonoBehaviour, IJumpController
{
    [RequireInterface(typeof(IJumpController))]
    [SerializeField]
    private Object _jumpControllerObject;
    private IJumpController JumpController => _jumpControllerObject as IJumpController;

    [RequireInterface(typeof(IMovementConstraint))]
    [SerializeField]
    private Object _movementConstraintObject;
    private IMovementConstraint MovementConstraint => _movementConstraintObject as IMovementConstraint;

    public IMovementPerformer GetCancelerPerformer() => JumpController.GetCancelerPerformer();
    public IMovementPerformer GetGravityPerformer() => JumpController.GetGravityPerformer();
    public IMovementPerformer GetImpulsePerformer() => MovementConstraint.CanPerformMovement()
                                                       ? JumpController.GetImpulsePerformer()
                                                       : null;
}