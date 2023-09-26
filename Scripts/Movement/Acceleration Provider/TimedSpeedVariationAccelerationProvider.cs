using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class TimedSpeedVariationAccelerationProvider : MonoBehaviour, IAccerlerationProvider, IDurationProvider
{
    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField]
    private Object _speedProviderObject;
    private ISpeedProvider SpeedProvider => _speedProviderObject as ISpeedProvider;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _durationProviderObject;
    private IDurationProvider DurationProvider => _durationProviderObject as IDurationProvider;

    public float GetAccelerationMagnitude() => SpeedProvider.GetSpeed() / (float)DurationProvider.GetDuration().TotalSeconds;
    public TimeSpan GetDuration() => DurationProvider.GetDuration();
}