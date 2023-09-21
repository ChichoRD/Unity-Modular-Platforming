using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class MovementThresholdingInputProvider : MonoBehaviour,
                                                 IMovementInputProvider<float>, IMovementInputProvider<Vector2>, IMovementInputProvider<Vector3>,
                                                 IObservableInputService
{
    //[RequireInterface(typeof(IMovementInputProvider<object>))]
    [SerializeReference]
    private Object _movementInputProviderObject;

    [SerializeField] private float _threshold = Mathf.Epsilon;
    public Func<Vector3, Func<float, bool>> ThresholdingMetric { get; set; } = (v) => (t) => v.magnitude > t;
    private Vector3 _input;
    public Vector3 Input
    {
        get => _input;
        set
        {
            Vector3 previousInput = _input;
            _input = value;

            bool wasPreviousInputValid = ThresholdingMetric(previousInput)(_threshold);
            bool isCurrentInputValid = ThresholdingMetric(_input)(_threshold);

            if (!wasPreviousInputValid && isCurrentInputValid)
                InputAppeared?.Invoke(this, EventArgs.Empty);
            if (wasPreviousInputValid && !isCurrentInputValid)
                InputDisappeared?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler InputAppeared;
    public event EventHandler InputDisappeared;

    public float GetMovementInput() => (Input = new Vector3(this.InputProviderFromObject<float, float>(_movementInputProviderObject).GetMovementInput(), 0.0f, 0.0f)).x;

    Vector2 IMovementInputProvider<Vector2>.GetMovementInput() => Input = this.InputProviderFromObject<Vector2, Vector2>(_movementInputProviderObject).GetMovementInput();

    Vector3 IMovementInputProvider<Vector3>.GetMovementInput() => Input = this.InputProviderFromObject<Vector3, Vector3>(_movementInputProviderObject).GetMovementInput();
}