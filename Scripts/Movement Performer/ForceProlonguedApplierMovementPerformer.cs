using System.Collections;
using UnityEngine;

public class ForceProlonguedApplierMovementPerformer : MonoBehaviour, IMovementPerformer
{
    [RequireInterface(typeof(IMovementInputProvider<Vector3>))]
    [SerializeField]
    private Object _movementInputProviderObject;
    private IMovementInputProvider<Vector3> MovementInputProvider => _movementInputProviderObject as IMovementInputProvider<Vector3>;

    [RequireInterface(typeof(IForceProvider))]
    [SerializeField]
    private Object _forceProviderObject;
    private IForceProvider ForceProvider => _forceProviderObject as IForceProvider;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _durationProviderObject;
    private IDurationProvider DurationProvider => _durationProviderObject as IDurationProvider;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField]
    private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    private Coroutine _prologuedForceCoroutine;

    public bool TryPerformMovement() => MovementInputProvider != null
                                        && ForceProvider != null
                                        && RigidbodyAccessor != null
                                        && (TryStartProlonguedForceCoroutine()
                                            || (TryStopProlonguedForceCoroutine()
                                                && TryStartProlonguedForceCoroutine()));  

    private IEnumerator ProlonguedForceCoroutine()
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        for (float t = 0.0f; t < DurationProvider.GetDuration().TotalSeconds; t += Time.fixedDeltaTime)
        {
            RigidbodyAccessor.AddForce(ForceProvider.GetForceMagnitude() * MovementInputProvider.GetMovementInput());
            yield return waitForFixedUpdate;
        }

        RigidbodyAccessor.AddForce(ForceProvider.GetForceMagnitude() * MovementInputProvider.GetMovementInput());
        _prologuedForceCoroutine = null;
    }

    private bool TryStartProlonguedForceCoroutine()
    {
        if (_prologuedForceCoroutine != null) return false;

        _prologuedForceCoroutine = StartCoroutine(ProlonguedForceCoroutine());
        return true;
    }   

    private bool TryStopProlonguedForceCoroutine()
    {
        if (_prologuedForceCoroutine == null) return false;

        StopCoroutine(_prologuedForceCoroutine);
        _prologuedForceCoroutine = null;
        return true;
    }
}