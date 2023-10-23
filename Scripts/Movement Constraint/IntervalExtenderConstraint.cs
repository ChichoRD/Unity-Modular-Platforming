using System.Collections;
using UnityEngine;

public class IntervalExtenderConstraint : MonoBehaviour, IMovementConstraint, ICachedProvider
{
    [RequireInterface(typeof(IMovementConstraint))]
    [SerializeField]
    private Object _movementConstraintObject;
    private IMovementConstraint MovementConstraint => _movementConstraintObject as IMovementConstraint;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _durationProviderObject;
    private IDurationProvider DurationProvider => _durationProviderObject as IDurationProvider;

    private bool _cachedValue;
    public bool CachedValue
    { 
        get => _cachedValue;
        set
        {
            if (_trueSkipsInterval && value)
            {
                _cachedValue = value;
                TryStopCachedValueAssignation();
                return;
            }

            if (_falseSkipsInterval && !value)
            {
                _cachedValue = value;
                TryStopCachedValueAssignation();
                return;
            }

            //TryStopCachedValueAssignation();
            TryStartCachedValueAssignation(value);
        }
    }

    private Coroutine _delayedCacheAssignationCoroutine;

    [SerializeField]
    private bool _trueSkipsInterval;
    [SerializeField]
    private bool _falseSkipsInterval;

    public bool CanPerformMovement() => CachedValue;
    public void UpdateCache() => CachedValue = MovementConstraint.CanPerformMovement();

    private bool TryStopCachedValueAssignation()
    {
        if (_delayedCacheAssignationCoroutine == null) return false;
        StopCoroutine(_delayedCacheAssignationCoroutine);
        _delayedCacheAssignationCoroutine = null;
        return true;
    }

    private bool TryStartCachedValueAssignation(bool value)
    {
        if (_delayedCacheAssignationCoroutine != null) return false;
        _delayedCacheAssignationCoroutine = StartCoroutine(AssingCacheAfterDelay(value));
        return true;
    }

    private IEnumerator AssingCacheAfterDelay(bool value)
    {
        yield return new WaitForSeconds((float)(DurationProvider?.GetDuration().TotalSeconds ?? 0.0f));
        _cachedValue = value;
        _delayedCacheAssignationCoroutine = null;
    }
}
