using UnityEngine;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class SettingGravityProfile2D : ScriptableObject, IAccerlerationProvider
{
    private const string NAME = "Setting Gravity Profile 2D";
    private const string PATH = "Movement Profiles/" + NAME;

    [field: SerializeField] public Vector2 ProjectionAxis { get; set; } = Vector2.up;
    public float GetAccelerationMagnitude() => Vector2.Dot(ProjectionAxis, Physics2D.gravity);
}
