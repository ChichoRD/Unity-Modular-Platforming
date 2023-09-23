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

    [RequireInterface(typeof(IAccerlerationProfile))]
    [SerializeField]
    private Object _accelerationProfile;
    private IAccerlerationProfile AccelerationProfile => _accelerationProfile as IAccerlerationProfile;

    [RequireInterface(typeof(IAccerlerationProfile))]
    [SerializeField]
    private Object _decelerationProfile;
    private IAccerlerationProfile DecelerationProfile => _decelerationProfile as IAccerlerationProfile;

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

    private void ObservableInputService_InputAppeared(object sender, System.EventArgs e) => Acceleratable.BeginAcceleration(AccelerationProfile);
    private void ObservableInputService_InputDisappeared(object sender, System.EventArgs e) => Acceleratable.BeginAcceleration(DecelerationProfile);
}