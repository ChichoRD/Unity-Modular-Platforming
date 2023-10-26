using UnityEngine.Events;

public interface IObservablePlanarMovementController
{
    UnityEvent AccelerationStarted { get; }
    UnityEvent AccelerationEnded { get; }
    UnityEvent DecelerationStarted { get; }
    UnityEvent DecelerationEnded { get; }
}
