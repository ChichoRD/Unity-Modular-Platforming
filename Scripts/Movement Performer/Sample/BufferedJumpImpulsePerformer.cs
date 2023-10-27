using System.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;
using Debug = UnityEngine.Debug;
using System;

public class BufferedJumpImpulsePerformer : MonoBehaviour, IMovementPerformer
{
    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _movementPerformerObject;
    private IMovementPerformer MovementPerformer => _movementPerformerObject as IMovementPerformer;

    [RequireInterface(typeof(IObservableJumpController))]
    [SerializeField]
    private Object _observableJumpControllerObject;
    private IObservableJumpController ObservableJumpController => _observableJumpControllerObject as IObservableJumpController;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _durationProviderObject;
    private IDurationProvider DurationProvider => _durationProviderObject as IDurationProvider;

    private IRigidbodyAccessor _lastRigidbodyAccessor;
    private readonly Stopwatch _lastPerformedMovementStopwatch = new Stopwatch();

    private void Awake()
    {
        ObservableJumpController.DescentEnded.AddListener(OnDescentEnded);
    }

    private void OnDestroy()
    {
        ObservableJumpController.DescentEnded.RemoveListener(OnDescentEnded);
    }

    public bool TryPerformMovement(IRigidbodyAccessor rigidbodyAccessor) =>
        MovementPerformer.TryPerformMovement(_lastRigidbodyAccessor = rigidbodyAccessor)
        || (TryStartLastPerformedMovementStopwatch()
            && false);

    private bool TryStartLastPerformedMovementStopwatch()
    {
        if (_lastPerformedMovementStopwatch.IsRunning) return false;

        _lastPerformedMovementStopwatch.Restart();
        return true;
    }

    private bool TryStopLastPerformedMovementStopwatch()
    {
        if (!_lastPerformedMovementStopwatch.IsRunning) return false;

        _lastPerformedMovementStopwatch.Reset();
        return true;
    }

    private bool IsLastPerformedMovementWithinDuration() =>
        _lastPerformedMovementStopwatch.IsRunning
        && _lastPerformedMovementStopwatch.Elapsed.TotalSeconds < DurationProvider.GetDuration().TotalSeconds;

    private void OnDescentEnded()
    {
        _ = !IsLastPerformedMovementWithinDuration()
            || (TryPerformMovement(_lastRigidbodyAccessor)
                && TryStopLastPerformedMovementStopwatch());
    }
}