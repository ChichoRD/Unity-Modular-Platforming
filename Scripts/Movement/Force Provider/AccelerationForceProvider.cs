using UnityEngine;

public class AccelerationForceProvider : MonoBehaviour, IForceProvider
{
    [RequireInterface(typeof(IAccerlerationProvider))]
    [SerializeField]
    private Object _accelerationProfileObject;
    private IAccerlerationProvider AccelerationProfile => _accelerationProfileObject as IAccerlerationProvider;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessorObject => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public float GetTargetForceMagnitude() => RigidbodyAccessorObject.Mass * AccelerationProfile.GetAcceleration();
}
