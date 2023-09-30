using UnityEngine;
using UnityEngine.InputSystem;

public class JumpTestMovement : MonoBehaviour
{
    [RequireInterface(typeof(IMovementInputReader<Vector3>))]
    [SerializeField]
    private Object _jumpInputReaderObject;
    private IMovementInputReader<Vector3> JumpInputReader => _jumpInputReaderObject as IMovementInputReader<Vector3>;

    [RequireInterface(typeof(IJumpController))]
    [SerializeField]
    private Object _jumpControllerObject;
    private IJumpController JumpController => _jumpControllerObject as IJumpController;

    [SerializeField] private Vector3 _defaultJumpDirection = Vector3.up;

    [SerializeField] private InputActionReference _jumpPerformedInputAction;
    [SerializeField] private InputActionReference _jumpCanceledInputAction;

    private void Awake()
    {
        _jumpPerformedInputAction.action.performed += JumpPerformed;
        _jumpCanceledInputAction.action.performed += JumpCanceled;
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
        _jumpPerformedInputAction.action.performed -= JumpPerformed;
        _jumpCanceledInputAction.action.performed -= JumpCanceled;
    }

    private void Start()
    {
        JumpInputReader.SetMovementInput(_defaultJumpDirection);
    }

    private void FixedUpdate()
    {
        JumpController.GetGravityPerformer()?.TryPerformMovement();
    }

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        JumpController.GetImpulsePerformer()?.TryPerformMovement();
    }

    private void JumpCanceled(InputAction.CallbackContext context)
    {
        JumpController.GetCancelerPerformer()?.TryPerformMovement();
    }
}