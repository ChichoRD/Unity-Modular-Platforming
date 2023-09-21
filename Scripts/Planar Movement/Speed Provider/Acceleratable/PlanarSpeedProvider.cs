using UnityEngine;

public class PlanarSpeedProvider : MonoBehaviour, IPlanarSpeedProvider
{
    [RequireInterface(typeof(ISpeedProfile))]
    [SerializeField] private Object _speedProfileObject;
    private ISpeedProfile SpeedProfile => _speedProfileObject as ISpeedProfile;

    public float GetPlanarTargetSpeed() => SpeedProfile.MaxSpeed;
}