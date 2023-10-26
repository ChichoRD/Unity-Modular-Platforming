using UnityEngine;

public class AccelerationForceProvider : MonoBehaviour, IForceProvider
{
    [RequireInterface(typeof(IAccerlerationProvider))]
    [SerializeField]
    private Object _accelerationProviderObject;
    private IAccerlerationProvider AccelerationProvider => _accelerationProviderObject as IAccerlerationProvider;

    public float GetForceMagnitude(IRigidbodyAccessor rigidbodyAccessor) => rigidbodyAccessor.Mass * AccelerationProvider.GetAccelerationMagnitude();
}
