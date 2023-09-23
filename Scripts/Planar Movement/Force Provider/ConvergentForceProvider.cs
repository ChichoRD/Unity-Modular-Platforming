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

    [RequireInterface(typeof(ISpeedMetric))]
    [SerializeField]
    private Object _speedMetricObject;
    private ISpeedMetric SpeedMetric => _speedMetricObject as ISpeedMetric;


    public float GetPlanarTargetForceMagnitude()
    {
        float targetSpeed = PlanarSpeedProvider.GetPlanarTargetSpeed();
        float currentSpeed = SpeedMetric.MeasureSpeed(RigidbodyAccessor.Velocity);

        const float CONVERGENCE_FACTOR = 0.5f;
        float speedIncrement = (targetSpeed - currentSpeed) * CONVERGENCE_FACTOR;

        return RigidbodyAccessor.Mass
               * speedIncrement
               / Time.fixedDeltaTime;
    }
}
