using UnityEngine;

public class BoxCaster3D : MonoBehaviour, IPhysicscaster3D
{
    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private Vector3 _size;

    [SerializeField]
    private Vector3 _center;

    [SerializeField]
    private Vector3 _direction;

    [SerializeField]
    private float _distance;

    [SerializeField]
    private QueryTriggerInteraction _queryTriggerInteraction;

    public bool Cast(out RaycastHit raycastHit, LayerMask layerMask) => Physics.BoxCast(_transform.position + _center,
                                                                                        _size * 0.5f,
                                                                                        _direction.normalized,
                                                                                        out raycastHit,
                                                                                        _transform.rotation,
                                                                                        _distance,
                                                                                        layerMask,
                                                                                        _queryTriggerInteraction);

    private void OnDrawGizmosSelected()
    {
        if (_transform == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(_transform.position + _center, _transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, _size);
        Gizmos.DrawRay(Vector3.zero, _direction.normalized * _distance);
    }
}