using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class CachedRayFieldProvider : MonoBehaviour, IRayFieldProvider
{
    [RequireInterface(typeof(IRayFieldProvider))]
    [SerializeField]
    private Object _rayFieldProviderObject;
    private IRayFieldProvider RayFieldProivder => _rayFieldProviderObject as IRayFieldProvider;
    private IEnumerable<SizedRay> _rayField;

    public IEnumerable<SizedRay> GetRayField() => _rayField;
    public void UpdateRayField() => _rayField = RayFieldProivder.GetRayField();
}