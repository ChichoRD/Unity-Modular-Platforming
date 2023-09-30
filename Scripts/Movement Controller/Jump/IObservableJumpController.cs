using UnityEngine.Events;

public interface IObservableJumpController
{
    UnityEvent AscentStarted { get; }
    UnityEvent DescentStarted { get; }
    UnityEvent AscentEnded { get; }
    UnityEvent DescentEnded { get; }
}