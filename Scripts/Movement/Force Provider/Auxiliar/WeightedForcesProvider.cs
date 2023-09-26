using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class WeightedForcesProvider : MonoBehaviour, IForceProvider
{
    [Serializable]
    private struct WeightedForceProvider
    {
        [RequireInterface(typeof(IForceProvider))]
        [SerializeField]
        private Object _forceProviderObject;
        public readonly IForceProvider ForceProvider => _forceProviderObject as IForceProvider;

        [field: SerializeField]
        public float Weight { get; private set; }
    }

    [SerializeField] private WeightedForceProvider[] _forceProviders;
    public float GetForceMagnitude() => _forceProviders.Sum(provider => provider.ForceProvider.GetForceMagnitude() * provider.Weight);
}