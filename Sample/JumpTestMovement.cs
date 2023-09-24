using UnityEngine;
using UnityEngine.InputSystem;

public class JumpTestMovement : MonoBehaviour
{
    [RequireInterface(typeof(IMovementInputReader<Vector3>))]
    [SerializeField]
    private Object _movementInputReaderObject;
    private IMovementInputReader<Vector3> MovementInputReader => _movementInputReaderObject as IMovementInputReader<Vector3>;

    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _movementPerformerObject;
    private IMovementPerformer MovementPerformer => _movementPerformerObject as IMovementPerformer;

    [SerializeField] private InputActionReference _jumpMovementInputAction;

    private void Awake()
    {
        _jumpMovementInputAction.action.performed += JumpAction_Performed;
        _jumpMovementInputAction.action.canceled += JumpAction_Canceled;
    }

    private void OnEnable()
    {
        _jumpMovementInputAction.action.Enable();
    }

    private void OnDisable()
    {
        _jumpMovementInputAction.action.Disable();
    }

    private void OnDestroy()
    {
        _jumpMovementInputAction.action.performed -= JumpAction_Performed;
        _jumpMovementInputAction.action.canceled -= JumpAction_Canceled;
    }

    private void JumpAction_Performed(InputAction.CallbackContext obj)
    {
        MovementInputReader.SetMovementInput(Vector3.up);
        MovementPerformer.TryPerformMovement();
    }

    private void JumpAction_Canceled(InputAction.CallbackContext context)
    {
        return;
        throw new System.NotImplementedException();
    }
}