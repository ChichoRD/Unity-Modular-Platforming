using UnityEngine;

public class InputAlignmentSpeedMetric : MonoBehaviour, ISpeedMetric
{
    [RequireInterface(typeof(IMovementInputProvider<Vector3>))]
    [SerializeField]
    private Object inputProviderObject;
    private IMovementInputProvider<Vector3> InputProvider => inputProviderObject as IMovementInputProvider<Vector3>;

    public float MeasureSpeed(Vector3 velocity) => Vector3.Dot(velocity, InputProvider.GetMovementInput());
}