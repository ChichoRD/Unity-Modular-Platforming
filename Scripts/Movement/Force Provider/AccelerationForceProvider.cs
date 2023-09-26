using UnityEngine;

public class AccelerationForceProvider : MonoBehaviour, IForceProvider
{
    [RequireInterface(typeof(IAccerlerationProvider))]
    [SerializeField]
    private Object _accelerationProviderObject;
    private IAccerlerationProvider AccelerationProvider => _accelerationProviderObject as IAccerlerationProvider;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public float GetForceMagnitude() => RigidbodyAccessor.Mass * AccelerationProvider.GetAccelerationMagnitude();
}
