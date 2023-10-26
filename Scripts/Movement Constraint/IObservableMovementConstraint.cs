using UnityEngine.Events;

public interface IObservableMovementConstraint
{
    UnityEvent MovementBecomeAvailable { get; }
    UnityEvent MovementBecomeUnavailable { get; }
}