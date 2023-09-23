using UnityEngine;
using UnityEngine.Events;

public class InputThresholdingObserver : MonoBehaviour
{
    [SerializeField] private UnityEvent _inputAppeared = new UnityEvent();
    [SerializeField] private UnityEvent _inputDisappeared = new UnityEvent();

    [RequireInterface(typeof(IObservableInputService))]
    [SerializeField]
    private Object _observableInputService;
    private IObservableInputService ObservableInputService => _observableInputService as IObservableInputService;

    private void Awake()
    {
        ObservableInputService.InputAppeared += ObservableInputService_InputAppeared;
        ObservableInputService.InputDisappeared += ObservableInputService_InputDisappeared;
    }

    private void OnDestroy()
    {
        ObservableInputService.InputAppeared -= ObservableInputService_InputAppeared;
        ObservableInputService.InputDisappeared -= ObservableInputService_InputDisappeared;
    }

    private void ObservableInputService_InputAppeared(object sender, System.EventArgs e) => _inputAppeared?.Invoke();
    private void ObservableInputService_InputDisappeared(object sender, System.EventArgs e) => _inputDisappeared?.Invoke();
}
