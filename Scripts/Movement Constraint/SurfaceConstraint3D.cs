using UnityEngine;

public class SurfaceConstraint3D : MonoBehaviour, IMovementConstraint // TODO - Segregate
{
    [RequireInterface(typeof(IPhysicscaster3D))]
    [SerializeField]
    private Object _physicscasterObject;
    private IPhysicscaster3D Physicscaster => _physicscasterObject as IPhysicscaster3D;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private Vector3 _surfaceComparerNormal = Vector3.up;

    [SerializeField]
    private float _minAngle;

    [SerializeField]
    private float _maxAngle;

    public bool CanPerformMovement() => Physicscaster.Cast(out RaycastHit raycastHit, _layerMask)
                                        && IsAngleBetween(raycastHit.normal, _minAngle, _maxAngle);

    private bool IsAngleBetween(Vector3 normal, float minAngle, float maxAngle)
    {
        float angle = Vector3.Angle(normal, _surfaceComparerNormal);
        return angle >= minAngle && angle <= maxAngle;
    }
}
