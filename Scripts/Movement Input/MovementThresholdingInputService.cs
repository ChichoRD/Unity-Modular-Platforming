using System;
using UnityEngine;

public class MovementThresholdingInputService : MonoBehaviour,
                                                 IMovementInputReader<float>, IMovementInputReader<Vector2>, IMovementInputReader<Vector3>,
                                                 IMovementInputProvider<float>, IMovementInputProvider<Vector2>, IMovementInputProvider<Vector3>,
                                                 IObservableInputService
{
    [SerializeField] private float _threshold = Mathf.Epsilon;
    public Func<Vector3, Func<float, bool>> ThresholdingMetric { get; set; } = (v) => (t) => v.magnitude > t;

    private Vector3 _lastValidInput;
    private Vector3 _input;
    public Vector3 Input
    {
        get => ThresholdingMetric(_input)(_threshold) ? _input : _lastValidInput;
        set
        {
            Vector3 previousInput = _input;
            _input = value;

            bool wasPreviousInputValid = ThresholdingMetric(previousInput)(_threshold);
            bool isCurrentInputValid = ThresholdingMetric(_input)(_threshold);

            if (!wasPreviousInputValid && isCurrentInputValid)
                InputAppeared?.Invoke(this, EventArgs.Empty);
            if (wasPreviousInputValid && !isCurrentInputValid)
            {
                _lastValidInput = previousInput;
                InputDisappeared?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public event EventHandler InputAppeared;
    public event EventHandler InputDisappeared;

    public void SetMovementInput(float input) => Input = new Vector3(input, 0.0f, 0.0f);
    public void SetMovementInput(Vector2 input) => Input = input;
    public void SetMovementInput(Vector3 input) => Input = input;

    public float GetMovementInput() => Input.x;
    Vector2 IMovementInputProvider<Vector2>.GetMovementInput() => Input;
    Vector3 IMovementInputProvider<Vector3>.GetMovementInput() => Input;
}