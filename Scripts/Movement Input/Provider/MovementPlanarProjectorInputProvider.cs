using UnityEngine;

public class MovementPlanarProjectorInputProvider : MonoBehaviour,
                                                    IMovementInputProvider<float>, IMovementInputProvider<Vector2>, IMovementInputProvider<Vector3>
{
    //[RequireInterface(typeof(IMovementInputProvider<object>))]
    [SerializeReference]
    private Object _movementInputProviderObject;

    [field: SerializeField] public Vector3 ProjectionNormal { get; set; } = Vector3.up;
    [SerializeField] private bool _renormalize = true;

    public float GetMovementInput()
    {
        Vector3 projectedInput = Vector3.ProjectOnPlane(new Vector3(this.InputProviderFromObject<float, float>(_movementInputProviderObject).GetMovementInput(), 0.0f, 0.0f), ProjectionNormal);
        return _renormalize ? projectedInput.normalized.x : projectedInput.x;
    }

    Vector2 IMovementInputProvider<Vector2>.GetMovementInput()
    {
        Vector2 projectedInput = Vector3.ProjectOnPlane(this.InputProviderFromObject<Vector2, Vector2>(_movementInputProviderObject).GetMovementInput(), ProjectionNormal);
        return _renormalize ? projectedInput.normalized : projectedInput;
    }

    Vector3 IMovementInputProvider<Vector3>.GetMovementInput()
    {
        Vector3 projectedInput = Vector3.ProjectOnPlane(this.InputProviderFromObject<Vector3, Vector3>(_movementInputProviderObject).GetMovementInput(), ProjectionNormal);
        return _renormalize ? projectedInput.normalized : projectedInput;
    }
}