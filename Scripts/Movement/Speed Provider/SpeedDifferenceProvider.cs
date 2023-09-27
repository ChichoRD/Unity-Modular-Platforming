using UnityEngine;

public class SpeedDifferenceProvider : MonoBehaviour, ISpeedProvider
{
    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField]
    private Object _speedProviderObject;
    private ISpeedProvider SpeedProvider => _speedProviderObject as ISpeedProvider;

    [RequireInterface(typeof(ISpeedMetric))]
    [SerializeField]
    private Object _speedMetricObject;
    private ISpeedMetric SpeedMetric => _speedMetricObject as ISpeedMetric;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public float GetSpeed() => SpeedProvider.GetSpeed() - SpeedMetric.MeasureSpeed(RigidbodyAccessor.Velocity);
}