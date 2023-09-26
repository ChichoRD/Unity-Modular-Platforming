using UnityEngine;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class ConstantAccelerationProfileObject : ScriptableObject, IAccerlerationProvider
{
    private const string NAME = "Constant Acceleration Profile";
    private const string PATH = "Movement Profiles/" + NAME;

    [SerializeField] private float _acceleration;
    public float GetAccelerationMagnitude() => _acceleration;
}