using UnityEngine;

public class SpeedVariationForceProfileObject : ScriptableObject, IForceProfile
{
    [SerializeField] private float _speedVariation = 1.0f;
    [SerializeField] private float _speedVariationDuration = 1.0f;
    [SerializeField] private float _affectedBodyMass = 1.0f;

    public float ForceMagnitude => _affectedBodyMass * _speedVariation / _speedVariationDuration;
}