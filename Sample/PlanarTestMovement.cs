using UnityEngine;
using UnityEngine.InputSystem;

public class PlanarTestMovement : MonoBehaviour
{
    [RequireInterface(typeof(IMovementInputReader<Vector2>))]
    [SerializeField]
    private Object _movementInputReaderObject;
    private IMovementInputReader<Vector2> MovementInputReader => _movementInputReaderObject as IMovementInputReader<Vector2>;

    [RequireInterface(typeof(IMovementPerformer))]
    [SerializeField]
    private Object _movementPerformerObject;
    private IMovementPerformer MovementPerformer => _movementPerformerObject as IMovementPerformer;

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
        MovementPerformer.TryPerformMovement();
    }
}
