using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class IntervalJumpObserverConstraint : MonoBehaviour, IMovementConstraint
{
    [RequireInterface(typeof(IObservableJumpController))]
    [SerializeField]
    private Object _observableJumpControllerObject;
    private IObservableJumpController ObservableJumpController => _observableJumpControllerObject as IObservableJumpController;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _durationProviderObject;
    private IDurationProvider DurationProvider => _durationProviderObject as IDurationProvider;

    [SerializeField]
    private bool _valueOnPeriod;

    [Serializable]
    private enum ListenedJumpEvent
    {
        AscentStarted,
        AscentEnded,
        DescentStarted,
        DescentEnded,
    }

    [SerializeField]
    private ListenedJumpEvent _listenedJumpEvent;

    private Coroutine _jumpIntervalCoroutine;
    private UnityEvent _selectedJumpEvent;

    private void Awake()
    {
        _selectedJumpEvent = _listenedJumpEvent switch
        {
            ListenedJumpEvent.AscentStarted => ObservableJumpController.AscentStarted,
            ListenedJumpEvent.AscentEnded => ObservableJumpController.AscentEnded,
            ListenedJumpEvent.DescentStarted => ObservableJumpController.DescentStarted,
            ListenedJumpEvent.DescentEnded => ObservableJumpController.DescentEnded,
            _ => throw new NotImplementedException(),
        };

        _selectedJumpEvent.AddListener(RunJumpIntervalCoroutine);
    }

    private void OnDestroy()
    {
        _selectedJumpEvent.RemoveListener(RunJumpIntervalCoroutine);
    }

    private bool TryStopJumpIntervalCoroutine()
    {
        if (_jumpIntervalCoroutine == null) return false;
        StopCoroutine(_jumpIntervalCoroutine);
        _jumpIntervalCoroutine = null;
        return true;
    }

    private bool TryStartJumpIntervalCoroutine()
    {
        if (_jumpIntervalCoroutine != null) return false;

        _jumpIntervalCoroutine = StartCoroutine(JumpIntervalCoroutine());
        return true;
    }

    private void RunJumpIntervalCoroutine() => _ = TryStopJumpIntervalCoroutine() & TryStartJumpIntervalCoroutine();

    private IEnumerator JumpIntervalCoroutine()
    {
        yield return new WaitForSeconds((float)(DurationProvider?.GetDuration().TotalSeconds ?? 0.0f));
        _jumpIntervalCoroutine = null;
    }

    public bool CanPerformMovement() => _jumpIntervalCoroutine == null || _valueOnPeriod;
}
