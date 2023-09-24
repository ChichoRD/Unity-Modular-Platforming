using UnityEngine;

public interface IAccerlerationCurveProvider
{
    float GetAccelerationTime();
    AnimationCurve AccelerationCurve { get; }
}
