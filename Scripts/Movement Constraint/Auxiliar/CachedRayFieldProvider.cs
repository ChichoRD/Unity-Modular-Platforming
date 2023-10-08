using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class CachedRayFieldProvider : MonoBehaviour, IRayFieldProvider, ICachedProvider
{
    [RequireInterface(typeof(IRayFieldProvider))]
    [SerializeField]
    private Object _rayFieldProviderObject;
    private IRayFieldProvider RayFieldProivder => _rayFieldProviderObject as IRayFieldProvider;
    private IEnumerable<SizedRay> _rayField = new SizedRay[0];

    public IEnumerable<SizedRay> GetRayField() => _rayField;
    public void UpdateCache() => _rayField = RayFieldProivder.GetRayField();
}
