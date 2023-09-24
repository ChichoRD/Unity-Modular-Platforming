using UnityEngine;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class ConstantAccelerationCurveProfileObject : ScriptableObject, IAccerlerationCurveProvider
{
    private const string NAME = "Constant Acceleration Curve Profile";
    private const string PATH = "Movement Profiles/" + NAME;

    [SerializeField] private float _accelerationTime;
    [SerializeField] private AnimationCurve _accelerationCurve;

    public float GetAccelerationTime() => _accelerationTime;
    public AnimationCurve AccelerationCurve => _accelerationCurve;
}