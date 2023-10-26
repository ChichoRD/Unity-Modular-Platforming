using UnityEngine;
using UnityEngine.InputSystem;

public class PlanarTestMovement : MonoBehaviour
{
    [RequireInterface(typeof(IMovementInputReader<Vector2>))]
    [SerializeField]
    private Object _movementInputReaderObject;
    private IMovementInputReader<Vector2> MovementInputReader => _movementInputReaderObject as IMovementInputReader<Vector2>;

    [RequireInterface(typeof(IPlanarMovementController))]
    [SerializeField]
    private Object _movementControllerObject;
    private IPlanarMovementController MovementController => _movementControllerObject as IPlanarMovementController;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;


    [SerializeField] private InputActionReference _planarMovementInputAction;

    private void OnEnable()
    {
        _planarMovementInputAction.action.Enable();
    }

    private void OnDisable()
    {
        _planarMovementInputAction.action.Disable();
    }

    private void Update()
    {
        MovementInputReader.SetMovementInput(_planarMovementInputAction.action.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        MovementController.GetPlanarMovementPerformer()?.TryPerformMovement(RigidbodyAccessor);
    }
}