using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class ObservableJumpController : MonoBehaviour, IJumpController, IObservableJump
{
    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _jumpImpulsePerformerObject;
    private IMovementPerformer JumpImpulsePerformer => _jumpImpulsePerformerObject as IMovementPerformer;

    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _jumpGravityPerformerObject;
    private IMovementPerformer JumpGravityPerformer => _jumpGravityPerformerObject as IMovementPerformer;

    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _jumpFallExtraGravityPerformerObject;
    private IMovementPerformer JumpFallExtraGravityPerformer => _jumpFallExtraGravityPerformerObject as IMovementPerformer;

    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _jumpCancelerPerformerObject;
    private IMovementPerformer JumpCancelerPerformer => _jumpCancelerPerformerObject as IMovementPerformer;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    [RequireInterface(typeof(ISpeedMetric))]
    [SerializeField]
    private Object _speedMetricObject;
    private ISpeedMetric SpeedMetric => _speedMetricObject as ISpeedMetric;

    [field: SerializeField] public UnityEvent AscentStarted { get; private set; } = new UnityEvent();
    [field: SerializeField] public UnityEvent DescentStarted { get; private set; } =  new UnityEvent();
    [field: SerializeField] public UnityEvent Landed { get; private set; } = new UnityEvent();

    private bool _descending;
    private bool _ascending;

    private float _currentJumpSpeed;
    public float CurrentJumpSpeed 
    { 
        get => _currentJumpSpeed; 
        set
        {
            float previousJumpSpeed = _currentJumpSpeed;
            _currentJumpSpeed = value;

            bool ascentStarted = previousJumpSpeed <= 0.0f && _currentJumpSpeed > 0.0f;
            bool descentStarted = previousJumpSpeed >= 0.0f && _currentJumpSpeed < 0.0f;

            bool landed = previousJumpSpeed < 0.0f && Mathf.Abs(_currentJumpSpeed) < 0.001f;

            if (ascentStarted)
                AscentStarted?.Invoke();
            if (descentStarted)
                DescentStarted?.Invoke();
            if (landed)
                Landed?.Invoke();
        }
    }

    private void Awake()
    {
        DescentStarted.AddListener(() => _descending = true);
        Landed.AddListener(() => _descending = false);

        AscentStarted.AddListener(() => _ascending = true);
        DescentStarted.AddListener(() => _ascending = false);
    }

    private void Update() => CurrentJumpSpeed = SpeedMetric.MeasureSpeed(RigidbodyAccessor.Velocity);

    public IMovementPerformer GetGravityPerformer() => _descending ? JumpFallExtraGravityPerformer : JumpGravityPerformer;
    public IMovementPerformer GetImpulsePerformer() => JumpImpulsePerformer;
    public IMovementPerformer GetCancelerPerformer() => _ascending ? JumpCancelerPerformer : null;
}
