using UnityEngine;

public class ProfileSpeedProvider : MonoBehaviour, ISpeedProvider
{
    [RequireInterface(typeof(ISpeedProfile))]
    [SerializeField] private Object _speedProfileObject;
    private ISpeedProfile SpeedProfile => _speedProfileObject as ISpeedProfile;

    public float GetTargetSpeed() => SpeedProfile.Speed;
}