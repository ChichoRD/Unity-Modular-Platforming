using UnityEngine;

public class ConvergentForceProvider : MonoBehaviour, IForceProvider
{
    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessor;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessor as IRigidbodyAccessor;

    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField]
    private Object _planarSpeedProvider;
    private ISpeedProvider PlanarSpeedProvider => _planarSpeedProvider as ISpeedProvider;

    [RequireInterface(typeof(ISpeedMetric))]
    [SerializeField]
    private Object _speedMetricObject;
    private ISpeedMetric SpeedMetric => _speedMetricObject as ISpeedMetric;


    public float GetTargetForceMagnitude()
    {
        float targetSpeed = PlanarSpeedProvider.GetSpeed();
        float currentSpeed = SpeedMetric.MeasureSpeed(RigidbodyAccessor.Velocity);

        const float CONVERGENCE_FACTOR = 0.5f;
        float speedIncrement = (targetSpeed - currentSpeed) * CONVERGENCE_FACTOR;

        return RigidbodyAccessor.Mass
               * speedIncrement
               / Time.fixedDeltaTime;
    }
}
