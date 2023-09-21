using UnityEngine;

public class MovementLinearMapperInputProvider : MonoBehaviour,
                                                 IMovementInputProvider<float>, IMovementInputProvider<Vector2>, IMovementInputProvider<Vector3>
{
    //[RequireInterface(typeof(IMovementInputProvider<object>))]
    [SerializeReference]
    private Object _movementInputProviderObject;

    [SerializeField] private Matrix4x4 _transformation = Matrix4x4.identity;

    public float GetMovementInput() => _transformation.MultiplyVector(new Vector3(this.InputProviderFromObject<float, float>(_movementInputProviderObject).GetMovementInput(), 0.0f, 0.0f)).x;
    Vector2 IMovementInputProvider<Vector2>.GetMovementInput() => _transformation.MultiplyVector(this.InputProviderFromObject<Vector2, Vector2>(_movementInputProviderObject).GetMovementInput());
    Vector3 IMovementInputProvider<Vector3>.GetMovementInput() => _transformation.MultiplyVector(this.InputProviderFromObject<Vector3, Vector3>(_movementInputProviderObject).GetMovementInput());
}
