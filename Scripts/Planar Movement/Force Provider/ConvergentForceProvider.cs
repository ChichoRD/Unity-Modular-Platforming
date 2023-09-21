using UnityEngine;

public class ConvergentForceProvider : MonoBehaviour, IPlanarForceProvider
{
    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessor;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessor as IRigidbodyAccessor;

    [RequireInterface(typeof(IPlanarSpeedProvider))]
    [SerializeField]
    private Object _planarSpeedProvider;
    private IPlanarSpeedProvider PlanarSpeedProvider => _planarSpeedProvider as IPlanarSpeedProvider;
    
    public float GetPlanarTargetForceMagnitude()
    {
        float targetSpeed = PlanarSpeedProvider.GetPlanarTargetSpeed();
        float currentSpeed = RigidbodyAccessor.Velocity.magnitude;
        float speedIncrement = targetSpeed - currentSpeed;

        const float MAXIMUM_SPEED_CLAMP_FACTOR = 0.5f;
        float maxSpeedIncrement = targetSpeed * MAXIMUM_SPEED_CLAMP_FACTOR;

        return RigidbodyAccessor.Mass
               * Mathf.Clamp(speedIncrement, -maxSpeedIncrement, maxSpeedIncrement)
               / Time.fixedDeltaTime;
    }
}