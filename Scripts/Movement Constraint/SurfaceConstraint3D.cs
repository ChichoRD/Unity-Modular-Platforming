using UnityEngine;

public class SurfaceConstraint3D : MonoBehaviour, IMovementConstraint // TODO - Segregate
{
    [RequireInterface(typeof(IRayFieldProvider))]
    [SerializeField]
    private Object _rayFieldProviderObject;
    private IRayFieldProvider RayFieldProivder => _rayFieldProviderObject as IRayFieldProvider;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private float _minAngle;

    [SerializeField]
    private float _maxAngle;

    public bool CanPerformMovement()
    {
        var rayField = RayFieldProivder.GetRayField();
        foreach (var ray in rayField)
        {
            if (!Physics.Raycast(ray, out RaycastHit hitInfo, ray.Size, _layerMask)) continue; // Abstract this

            float angle = Vector3.Angle(hitInfo.normal, Vector3.up);
            if (angle < _minAngle || angle > _maxAngle) continue; // Abstract this

            return true;
        }

        return false;
    }
}
