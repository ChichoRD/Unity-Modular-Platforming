using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ForceMovementPerformer : MonoBehaviour, IMovementPerformer
{
    [RequireInterface(typeof(IMovementInputProvider<Vector3>))]
    [SerializeField]
    private Object _movementInputProviderObject;
    private IMovementInputProvider<Vector3> MovementInputProvider => _movementInputProviderObject as IMovementInputProvider<Vector3>;

    [RequireInterface(typeof(IForceProvider))]
    [SerializeField]
    private Object _forceProviderObject;
    private IForceProvider ForceProvider => _forceProviderObject as IForceProvider;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public bool TryPerformMovement() => MovementInputProvider != null
                                        && ForceProvider != null
                                        && RigidbodyAccessor != null
                                        && new Func<bool>(() =>
                                        {
                                            RigidbodyAccessor.AddForce(ForceProvider.GetTargetForceMagnitude() * MovementInputProvider.GetMovementInput());
                                            return true;
                                        })();
}