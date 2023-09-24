using System.Collections;
using UnityEngine;

public class AcceleratableSpeedProvider : MonoBehaviour, ISpeedProvider, IAcceleratable
{
    [RequireInterface(typeof(ISpeedProvider))]
    [SerializeField] private Object _planarSpeedProviderObject;
    private ISpeedProvider PlanarSpeedProvider => _planarSpeedProviderObject as ISpeedProvider;

    private float _targetRelativeMovementSpeed;
    private Coroutine _accelerationCoroutine;

    public float GetSpeed() => _targetRelativeMovementSpeed * PlanarSpeedProvider.GetSpeed();

    public void BeginAcceleration(IAccerlerationCurveProvider accerlerationCurveProfile)
    {
        StopAccelerationCoroutine();

        _accelerationCoroutine = StartCoroutine(AccerlerateCoroutine(accerlerationCurveProfile));
    }

    private IEnumerator AccerlerateCoroutine(IAccerlerationCurveProvider accerlerationCurveProfile)
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        for (float t = 0.0f; t < accerlerationCurveProfile.GetAccelerationTime(); t += Time.fixedDeltaTime)
        {
            float normalizedTime = t / accerlerationCurveProfile.GetAccelerationTime();
            float targetRelativeMovementSpeed = accerlerationCurveProfile.AccelerationCurve.Evaluate(normalizedTime);

            _targetRelativeMovementSpeed = Mathf.Lerp(_targetRelativeMovementSpeed, targetRelativeMovementSpeed, normalizedTime);
            yield return wait;
        }

        _targetRelativeMovementSpeed = accerlerationCurveProfile.AccelerationCurve.Evaluate(1.0f);
        _accelerationCoroutine = null;
    }

    private bool StopAccelerationCoroutine()
    {
        if (_accelerationCoroutine == null) return false;

        StopCoroutine(_accelerationCoroutine);
        _accelerationCoroutine = null;
        return true;
    }
}