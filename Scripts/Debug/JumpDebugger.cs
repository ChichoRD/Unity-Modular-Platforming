using UnityEngine;

public class JumpDebugger : MonoBehaviour
{
    [SerializeField]
    private bool _debug = true;

    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField]
    private Object _planarSpeedProvider;
    private ISpeedProvider PlanarSpeedProvider => _planarSpeedProvider as ISpeedProvider;

    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField]
    private Object _verticalSpeedProvider;
    private ISpeedProvider VerticalSpeedProvider => _verticalSpeedProvider as ISpeedProvider;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _jumpAscentDurationProvider;
    private IDurationProvider JumpAscentDurationProvider => _jumpAscentDurationProvider as IDurationProvider;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _jumpDescentDurationProvider;
    private IDurationProvider JumpDescentDurationProvider => _jumpDescentDurationProvider as IDurationProvider;

    [RequireInterface(typeof(IAccerlerationProvider))]
    [SerializeField]
    private Object _ascentGravityProviderObject;
    private IAccerlerationProvider AscentGravityProvider => _ascentGravityProviderObject as IAccerlerationProvider;

    [RequireInterface(typeof(IAccerlerationProvider))]
    [SerializeField]
    private Object _descentGravityProviderObject;
    private IAccerlerationProvider DescentGravityProvider => _descentGravityProviderObject as IAccerlerationProvider;

    [SerializeField]
    private Color _debugColor = Color.green;

    [SerializeField]
    private Transform _debugTransform;

    [SerializeField]
    private Vector3 _planarDirection = Vector3.right;

    [SerializeField]
    private Vector3 _jumpDirection = Vector3.up;

    [SerializeField]
    private Vector3 _gravityDirection = Vector3.up;

    [SerializeField]
    [Min(float.Epsilon)]
    private float _debugStepDesity = 1.0f;

    private void OnDrawGizmos()
    {
        if (!_debug || !CanDebug()) return;

        GizmosDebug();
    }

    public void GizmosDebug()
    {
        Vector3 position = _debugTransform.position;
        Vector3 velocity = _debugTransform.TransformDirection(VerticalSpeedProvider.GetSpeed() * _jumpDirection) +
                           _debugTransform.TransformDirection(PlanarSpeedProvider.GetSpeed() * _planarDirection);
        Vector3 ascentAcceleration = _debugTransform.TransformDirection(AscentGravityProvider.GetAccelerationMagnitude() * _gravityDirection);
        Vector3 descentAcceleration = _debugTransform.TransformDirection(DescentGravityProvider.GetAccelerationMagnitude() * _gravityDirection);
        
        float delta = Time.fixedDeltaTime / _debugStepDesity;
        float duration = (float)(JumpAscentDurationProvider.GetDuration() + JumpDescentDurationProvider.GetDuration()).TotalSeconds;
        Gizmos.color = _debugColor;

        for (float t = 0.0f; t < duration; t += delta)
        {
            Vector3 previousPosition = position;
            Vector3 acceleration = Vector3.Dot(velocity, _jumpDirection) > 0.0f ? ascentAcceleration : descentAcceleration;

            // Verlet integration
            position += velocity * delta +
                        0.5f * delta * delta * acceleration;

            velocity += acceleration * delta;

            Gizmos.DrawLine(previousPosition, position);
        }
    }

    public bool CanDebug() => PlanarSpeedProvider != null &&
                              VerticalSpeedProvider != null &&
                              JumpAscentDurationProvider != null &&
                              JumpDescentDurationProvider != null &&
                              AscentGravityProvider != null &&
                              DescentGravityProvider != null &&
                              _debugTransform != null;
}