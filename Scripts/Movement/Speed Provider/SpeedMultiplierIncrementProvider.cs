using UnityEngine;

public class SpeedMultiplierIncrementProvider : MonoBehaviour, ISpeedProvider
{
    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    [RequireInterface(typeof(ISpeedMetric))]
    [SerializeField]
    private Object _speedMetricObject;
    private ISpeedMetric SpeedMetric => _speedMetricObject as ISpeedMetric;

    [SerializeField] private float _speedVariationFactor;
    public float GetSpeed()
    {
        float currentSpeed = SpeedMetric.MeasureSpeed(RigidbodyAccessor.Velocity);
        float targetSpeed = currentSpeed * _speedVariationFactor;

        return targetSpeed - currentSpeed;
    }
}