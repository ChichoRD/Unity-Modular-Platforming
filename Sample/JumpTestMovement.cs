using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class JumpTestMovement : MonoBehaviour
{
    [RequireInterface(typeof(IMovementInputReader<Vector3>))]
    [SerializeField]
    private Object _movementInputReaderObject;
    private IMovementInputReader<Vector3> MovementInputReader => _movementInputReaderObject as IMovementInputReader<Vector3>;

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

    [SerializeField] private InputActionReference _jumpPerformedInputAction;
    [SerializeField] private InputActionReference _jumpCanceledInputAction;

    [field: SerializeField] public UnityEvent AscentStarted { get; set; } = new UnityEvent();
    [field: SerializeField] public UnityEvent DescentStarted { get; set; } =  new UnityEvent();
    [field: SerializeField] public UnityEvent Landed { get; set; } = new UnityEvent();

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
        _jumpPerformedInputAction.action.performed += JumpAction_Performed;
        _jumpCanceledInputAction.action.performed += JumpAction_Canceled;

        DescentStarted.AddListener(() => _descending = true);
        Landed.AddListener(() => _descending = false);

        AscentStarted.AddListener(() => _ascending = true);
        DescentStarted.AddListener(() => _ascending = false);

        MovementInputReader.SetMovementInput(Vector3.up);
    }

    private void OnEnable()
    {
        _jumpPerformedInputAction.action.Enable();
        _jumpCanceledInputAction.action.Enable();
    }

    private void OnDisable()
    {
        _jumpPerformedInputAction.action.Disable();
        _jumpCanceledInputAction.action.Disable();
    }

    private void OnDestroy()
    {
        _jumpPerformedInputAction.action.performed -= JumpAction_Performed;
        _jumpCanceledInputAction.action.performed -= JumpAction_Canceled;
    }

    private void Update()
    {
        CurrentJumpSpeed = SpeedMetric.MeasureSpeed(RigidbodyAccessor.Velocity);
    }

    private void FixedUpdate()
    {
        JumpGravityPerformer.TryPerformMovement();

        if (!_descending) return;
        JumpFallExtraGravityPerformer.TryPerformMovement();
    }

    private void JumpAction_Performed(InputAction.CallbackContext obj)
    {
        JumpImpulsePerformer.TryPerformMovement();
    }

    private void JumpAction_Canceled(InputAction.CallbackContext context)
    {
        if (!_ascending) return;
        JumpCancelerPerformer.TryPerformMovement();
    }
}