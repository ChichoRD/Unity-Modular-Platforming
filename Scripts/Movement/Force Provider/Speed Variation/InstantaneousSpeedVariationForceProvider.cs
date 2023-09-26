using UnityEngine;

public class InstantaneousSpeedVariationForceProvider : MonoBehaviour, IForceProvider
{
    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField]
    private Object _speedVariationProviderObject;
    private ISpeedProvider SpeedVariationProvider => _speedVariationProviderObject as ISpeedProvider;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public float GetForceMagnitude() => RigidbodyAccessor.Mass * SpeedVariationProvider.GetSpeed() / Time.fixedDeltaTime;
}