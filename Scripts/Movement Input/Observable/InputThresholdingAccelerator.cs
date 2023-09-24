using UnityEngine;

public class InputThresholdingAccelerator : MonoBehaviour
{
    [RequireInterface(typeof(IObservableInputService))]
    [SerializeField]
    private Object _observableInputService;
    private IObservableInputService ObservableInputService => _observableInputService as IObservableInputService;

    [RequireInterface(typeof(IAcceleratable))]
    [SerializeField]
    private Object _acceleratable;
    private IAcceleratable Acceleratable => _acceleratable as IAcceleratable;

    [RequireInterface(typeof(IAccerlerationCurveProvider))]
    [SerializeField]
    private Object _accelerationCurveProvider;
    private IAccerlerationCurveProvider AccerlerationCurveProvider => _accelerationCurveProvider as IAccerlerationCurveProvider;

    [RequireInterface(typeof(IAccerlerationCurveProvider))]
    [SerializeField]
    private Object _decelerationCurveProvider;
    private IAccerlerationCurveProvider DeccerlerationCurveProvider => _decelerationCurveProvider as IAccerlerationCurveProvider;

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

    private void ObservableInputService_InputAppeared(object sender, System.EventArgs e) => Acceleratable.BeginAcceleration(AccerlerationCurveProvider);
    private void ObservableInputService_InputDisappeared(object sender, System.EventArgs e) => Acceleratable.BeginAcceleration(DeccerlerationCurveProvider);
}