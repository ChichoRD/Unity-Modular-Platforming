using UnityEngine;

public class ConstantForceProfileObject : ScriptableObject, IForceProfile
{
    [SerializeField] private float _forceMagnitude = 1.0f;
    public float ForceMagnitude => _forceMagnitude;
}
