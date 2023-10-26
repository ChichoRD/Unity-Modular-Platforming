using UnityEngine;
using Object = UnityEngine.Object;

public class WeightedForceProvider : MonoBehaviour, IForceProvider
{
    [RequireInterface(typeof(IForceProvider))]
    [SerializeField]
    private Object _forceProviderObject;
    private IForceProvider ForceProvider => _forceProviderObject as IForceProvider;

    [field: SerializeField]
    public float Weight { get; private set; }

    public float GetForceMagnitude(IRigidbodyAccessor rigidbodyAccessor) => ForceProvider.GetForceMagnitude(rigidbodyAccessor) * Weight;
}