using UnityEngine;

public class SpeedVariationForceProvider : MonoBehaviour, IForceProvider
{
    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField]
    private Object _speedProviderObject;
    private ISpeedProvider SpeedProvider => _speedProviderObject as ISpeedProvider;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessorObject => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public float GetTargetForceMagnitude() => RigidbodyAccessorObject.Mass * SpeedProvider.GetSpeed() / Time.fixedDeltaTime;
}