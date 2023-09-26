using UnityEngine;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class SettingGravityProfile3D : ScriptableObject, IAccerlerationProvider
{
    private const string NAME = "Setting Gravity Profile 3D";
    private const string PATH = "Movement Profiles/" + NAME;

    [field: SerializeField] public Vector3 ProjectionAxis { get; set; } = Vector3.up;
    public float GetAccelerationMagnitude() => Vector3.Dot(ProjectionAxis, Physics.gravity);
}