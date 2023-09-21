using UnityEngine;

public class MovementInputService : MonoBehaviour,
                                    IMovementInputReader<float>, IMovementInputReader<Vector2>, IMovementInputReader<Vector3>,
                                    IMovementInputProvider<float>, IMovementInputProvider<Vector2>, IMovementInputProvider<Vector3>
{
    private Vector3 _input = Vector3.zero;

    public float GetMovementInput() => _input.x;
    public void SetMovementInput(float input) => _input = new Vector3(input, 0.0f, 0.0f);

    Vector2 IMovementInputProvider<Vector2>.GetMovementInput() => _input;
    public void SetMovementInput(Vector2 input) => _input = input;

    Vector3 IMovementInputProvider<Vector3>.GetMovementInput() => _input;
    public void SetMovementInput(Vector3 input) => _input = input;
}
