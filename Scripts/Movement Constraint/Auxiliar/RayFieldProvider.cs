using System;
using System.Collections.Generic;
using UnityEngine;

public class RayFieldProvider : MonoBehaviour, IRayFieldProvider
{
    [SerializeField]
    private Vector3 _fieldSize;

    [SerializeField]
    private Vector3 _fieldOffset;

    [SerializeField]
    private Transform _fieldCenterTransform;

    [SerializeField]
    private float _fieldDensity;

    [SerializeField]
    private float _rayLength;

    [SerializeField]
    private Vector3 _defaultRayGeneralDirection = Vector3.down;

    public Func<Vector3, Vector3> GetRayDirectionFromLocalPosition { get; set; }

    private void SetDefaultRayFunction() => GetRayDirectionFromLocalPosition = (localPosition) =>
                                                                               Vector3.Lerp(localPosition.normalized, _defaultRayGeneralDirection,
                                                                                            Vector3.Dot(localPosition.normalized, _defaultRayGeneralDirection)).normalized;

    private void Awake() => SetDefaultRayFunction();

    public IEnumerable<SizedRay> GetRayField()
    {
        Vector3Int raysSize = new Vector3Int((int)(_fieldSize.x * _fieldDensity), (int)(_fieldSize.y * _fieldDensity), (int)(_fieldSize.z * _fieldDensity));
        List<SizedRay> rays = new List<SizedRay>(raysSize.x * raysSize.y * raysSize.z);

        for (int x = 0; x <= raysSize.x; x++)
        {
            for (int y = 0; y <= raysSize.y; y++)
            {
                for (int z = 0; z <= raysSize.z; z++)
                {
                    Vector3 rayOrigin = _fieldCenterTransform.position
                                        + _fieldOffset
                                        + new Vector3(x / _fieldDensity, y / _fieldDensity, z / _fieldDensity)
                                        - _fieldSize / 2.0f;
                    Vector3 rayDirection = GetRayDirectionFromLocalPosition(rayOrigin - _fieldCenterTransform.position);
                    rays.Add(new SizedRay(rayOrigin, rayDirection * _rayLength));
                }
            }
        }

        return rays;
    }

    private void OnDrawGizmosSelected()
    {
        if (_fieldCenterTransform == null)
            return;

        if (GetRayDirectionFromLocalPosition == null)
            SetDefaultRayFunction();

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_fieldCenterTransform.position + _fieldOffset, _fieldSize); 
        var rays = GetRayField();
        foreach (var ray in rays)
        {
            Gizmos.DrawRay(ray.Origin, ray.Direction * ray.Size);
        }
    }
}
