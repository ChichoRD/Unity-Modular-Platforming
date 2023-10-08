using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class LoopingCachedProviderUpdater : MonoBehaviour
{
    [RequireInterface(typeof(ICachedProvider))]
    [SerializeField]
    private Object _cachedProviderObject;
    private ICachedProvider CachedProvider => _cachedProviderObject as ICachedProvider;

    [SerializeField]
    private bool _beginUpdatesOnStart = true;

    [SerializeField]
    [Min(float.Epsilon)]
    private float _updateRate = 10.0f;

    private WaitForSeconds _waitForCacheUpdate;
    private Coroutine _updateCachedProviderCoroutine;

    private void Start()
    {
        _waitForCacheUpdate = new WaitForSeconds(1.0f / _updateRate);
        if (!_beginUpdatesOnStart) return;

        TryStartCacheUpdate();
    }

    private IEnumerator UpdateCachedProviderCoroutine()
    {
        while (enabled)
        {
            CachedProvider?.UpdateCache();
            yield return _waitForCacheUpdate;
        }

        _updateCachedProviderCoroutine = null;
    }

    public bool TryStopCacheUpdate()
    {
        if (_updateCachedProviderCoroutine == null) return false;
        StopCoroutine(_updateCachedProviderCoroutine);
        _updateCachedProviderCoroutine = null;
        return true;
    }

    public bool TryStartCacheUpdate()
    {
        if (_updateCachedProviderCoroutine != null) return false;
        _updateCachedProviderCoroutine = StartCoroutine(UpdateCachedProviderCoroutine());
        return true;
    }
}