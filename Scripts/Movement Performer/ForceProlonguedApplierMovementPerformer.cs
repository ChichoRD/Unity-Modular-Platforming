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

    private Coroutine _prologuedForceCoroutine;

    public bool TryPerformMovement(IRigidbodyAccessor rigidbodyAccessor) => 
        MovementInputProvider != null
        && ForceProvider != null
        && (TryStartProlonguedForceCoroutine(rigidbodyAccessor)
            || (TryStopProlonguedForceCoroutine()
                && TryStartProlonguedForceCoroutine(rigidbodyAccessor)));  

    private IEnumerator ProlonguedForceCoroutine(IRigidbodyAccessor rigidbodyAccessor)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        for (float t = 0.0f; t < DurationProvider.GetDuration().TotalSeconds; t += Time.fixedDeltaTime)
        {
            rigidbodyAccessor.AddForce(ForceProvider.GetForceMagnitude(rigidbodyAccessor) * MovementInputProvider.GetMovementInput());
            yield return waitForFixedUpdate;
        }

        rigidbodyAccessor.AddForce(ForceProvider.GetForceMagnitude(rigidbodyAccessor) * MovementInputProvider.GetMovementInput());
        _prologuedForceCoroutine = null;
    }

    private bool TryStartProlonguedForceCoroutine(IRigidbodyAccessor rigidbodyAccessor)
    {
        if (_prologuedForceCoroutine != null) return false;

        _prologuedForceCoroutine = StartCoroutine(ProlonguedForceCoroutine(rigidbodyAccessor));
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