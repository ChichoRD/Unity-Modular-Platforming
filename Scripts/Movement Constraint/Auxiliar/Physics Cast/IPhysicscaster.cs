using UnityEngine;

public interface IPhysicscaster3D
{
    bool Cast(out RaycastHit raycastHit, LayerMask layerMask);
}
