using UnityEngine;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class ConstantAccelerationProfileObject : ScriptableObject, IAccerlerationProfile
{
    private const string NAME = "Constant Acceleration Profile";
    private const string PATH = "Movement Profiles/" + NAME;

    [SerializeField] private float _accelerationTime;
    [SerializeField] private AnimationCurve _accelerationCurve;

    public float AccelerationTime => _accelerationTime;
    public AnimationCurve AccelerationCurve => _accelerationCurve;
}