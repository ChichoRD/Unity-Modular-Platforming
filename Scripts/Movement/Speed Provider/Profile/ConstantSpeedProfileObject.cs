using UnityEngine;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class ConstantSpeedProfileObject : ScriptableObject, ISpeedProvider
{
    private const string NAME = "Constant Speed Profile";
    private const string PATH = "Movement Profiles/" + NAME;

    [SerializeField] private float _speed;
    public float GetSpeed() => _speed;
}