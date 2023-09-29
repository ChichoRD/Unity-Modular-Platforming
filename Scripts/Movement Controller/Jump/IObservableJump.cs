using UnityEngine.Events;

public interface IObservableJump
{
    UnityEvent AscentStarted { get; }
    UnityEvent DescentStarted { get; }
    UnityEvent Landed { get; }
}