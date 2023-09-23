using UnityEngine;

public interface IAccerlerationProfile
{
    float AccelerationTime { get; }
    AnimationCurve AccelerationCurve { get; }
}
