using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlanarForceMovementPerformer : MonoBehaviour, IMovementPerformer
{
    [RequireInterface(typeof(IMovementInputProvider<Vector3>))]
    [SerializeField]
    private Object _movementInputServiceObject;
    private IMovementInputProvider<Vector3> MovementInputService => _movementInputServiceObject as IMovementInputProvider<Vector3>;

    [RequireInterface(typeof(IPlanarForceProvider))]
    [SerializeField]
    private Object _planarForceProviderObject;
    private IPlanarForceProvider PlanarForceProvider => _planarForceProviderObject as IPlanarForceProvider;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public bool TryPerformMovement() => MovementInputService != null
                                        && PlanarForceProvider != null
                                        && RigidbodyAccessor != null
                                        && new Func<bool>(() =>
                                        {
                                            RigidbodyAccessor.AddForce(PlanarForceProvider.GetPlanarTargetForceMagnitude() * MovementInputService.GetMovementInput());
                                            return true;
                                        })();
}