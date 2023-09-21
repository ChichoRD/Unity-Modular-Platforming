using System.Collections;
using UnityEngine;

public class AcceleratablePlanarSpeedProvider : MonoBehaviour, IPlanarSpeedProvider, IAcceleratable
{
    [RequireInterface(typeof(IPlanarSpeedProvider))]
    [SerializeField] private Object _planarSpeedProviderObject;
    private IPlanarSpeedProvider PlanarSpeedProvider => _planarSpeedProviderObject as IPlanarSpeedProvider;

    private float _targetRelativeMovementSpeed;
    private Coroutine _accelerationCoroutine;

    public float GetPlanarTargetSpeed() => _targetRelativeMovementSpeed * PlanarSpeedProvider.GetPlanarTargetSpeed();

    public void BeginAcceleration(IAccerlerationProfile accerlerationProfile)
    {
        StopAccelerationCoroutine();

        _accelerationCoroutine = StartCoroutine(AccerlerateCoroutine(accerlerationProfile));
    }

    private IEnumerator AccerlerateCoroutine(IAccerlerationProfile accerlerationProfile)
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        for (float t = 0.0f; t < accerlerationProfile.AccelerationTime; t += Time.fixedDeltaTime)
        {
            float normalizedTime = t / accerlerationProfile.AccelerationTime;
            float targetRelativeMovementSpeed = accerlerationProfile.AccelerationCurve.Evaluate(normalizedTime);

            _targetRelativeMovementSpeed = Mathf.Lerp(_targetRelativeMovementSpeed, targetRelativeMovementSpeed, normalizedTime);
            yield return wait;
        }

        _targetRelativeMovementSpeed = accerlerationProfile.AccelerationCurve.Evaluate(1.0f);
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