using UnityEngine;

public class ForceInstantaneousApplierMovementPerformer : MonoBehaviour, IMovementPerformer
{
    [RequireInterface(typeof(IMovementInputProvider<Vector3>))]
    [SerializeField]
    private Object _movementInputProviderObject;
    private IMovementInputProvider<Vector3> MovementInputProvider => _movementInputProviderObject as IMovementInputProvider<Vector3>;

    [RequireInterface(typeof(IForceProvider))]
    [SerializeField]
    private Object _forceProviderObject;
    private IForceProvider ForceProvider => _forceProviderObject as IForceProvider;

    public bool TryPerformMovement(IRigidbodyAccessor rigidbodyAccessor) => MovementInputProvider != null
                                                                            && ForceProvider != null
                                                                            && TryApplyForce(rigidbodyAccessor, ForceProvider.GetForceMagnitude(rigidbodyAccessor) * MovementInputProvider.GetMovementInput());

    private bool TryApplyForce(IRigidbodyAccessor rigidbodyAccessor, Vector3 force)
    {
        rigidbodyAccessor.AddForce(force);
        return true;
    }
}
