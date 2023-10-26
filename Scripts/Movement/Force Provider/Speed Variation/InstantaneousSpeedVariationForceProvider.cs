using UnityEngine;

public class InstantaneousSpeedVariationForceProvider : MonoBehaviour, IForceProvider
{
    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField]
    private Object _speedVariationProviderObject;
    private ISpeedProvider SpeedVariationProvider => _speedVariationProviderObject as ISpeedProvider;

    public float GetForceMagnitude(IRigidbodyAccessor rigidbodyAccessor) => rigidbodyAccessor.Mass * SpeedVariationProvider.GetSpeed() / Time.fixedDeltaTime;
}