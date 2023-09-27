using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class AdditiveForcesProvider : MonoBehaviour, IForceProvider
{
    [SerializeField]
    private Object[] _forceProviderObjects;
    private IForceProvider[] ForceProviders => _forceProviderObjects.Cast<IForceProvider>().ToArray();
    public float GetForceMagnitude() => ForceProviders.Sum(provider => provider.GetForceMagnitude());
}
