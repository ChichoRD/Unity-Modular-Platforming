using UnityEngine;
using UnityEngine.Events;

public class ObservablePlanarMovementController : MonoBehaviour, IPlanarMovementController, IObservablePlanarMovementController
{
    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _planarMovementPerformerObject;
    private IMovementPerformer PlanarMovementPerformer => _planarMovementPerformerObject as IMovementPerformer;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    [RequireInterface(typeof(ISpeedMetric))]
    [SerializeField]
    private Object _speedMetricObject;
    private ISpeedMetric SpeedMetric => _speedMetricObject as ISpeedMetric;

    private float _currentMovementSpeed;
    public float CurrentMovementSpeed
    {
        get => _currentMovementSpeed;
        set
        {
            float previousMovementSpeed = _currentMovementSpeed;
            _currentMovementSpeed = value;

            bool accelerationStarted = previousMovementSpeed <= 0.0f && _currentMovementSpeed > 0.0f;
            bool decelerationStarted = previousMovementSpeed >= 0.0f && _currentMovementSpeed < 0.0f;

            bool accelerationEnded = previousMovementSpeed > 0.0f && _currentMovementSpeed <= 0.0f;
            bool decelerationEnded = previousMovementSpeed < 0.0f && _currentMovementSpeed >= 0.0f;

            if (accelerationStarted)
                AccelerationStarted?.Invoke();
            if (decelerationStarted)
                DecelerationStarted?.Invoke();
            if (accelerationEnded)
                AccelerationEnded?.Invoke();
            if (decelerationEnded)
                DecelerationEnded?.Invoke();
        }
    }

    [field: SerializeField] public UnityEvent AccelerationStarted { get; private set; } = new UnityEvent();
    [field: SerializeField] public UnityEvent AccelerationEnded { get; private set; } = new UnityEvent();
    [field: SerializeField] public UnityEvent DecelerationStarted { get; private set; } = new UnityEvent();
    [field: SerializeField] public UnityEvent DecelerationEnded { get; private set; } = new UnityEvent();

    private void Update() => CurrentMovementSpeed = SpeedMetric.MeasureSpeed(RigidbodyAccessor.Velocity);

    public IMovementPerformer GetPlanarMovementPerformer() => PlanarMovementPerformer;
}
