using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ConstraintMerger : MonoBehaviour, IMovementConstraint
{
    [SerializeField]
    private Object[] _movementConstraintObjects;
    private IEnumerable<IMovementConstraint> MovementConstraints => _movementConstraintObjects.Cast<IMovementConstraint>();

    [Serializable]
    private enum ConstraintMergeType
    {
        And,
        Nand,
        Or,
        Xor,
        Xnor,
    }

    [SerializeField]
    private ConstraintMergeType _constraintType;
    public bool CanPerformMovement() => _constraintType switch
    {
                                           ConstraintMergeType.And => MovementConstraints.All(constraint => constraint.CanPerformMovement()),
                                           ConstraintMergeType.Nand => !MovementConstraints.All(constraint => constraint.CanPerformMovement()),
                                           ConstraintMergeType.Or => MovementConstraints.Any(constraint => constraint.CanPerformMovement()),
                                           ConstraintMergeType.Xor => MovementConstraints.Count(constraint => constraint.CanPerformMovement()) == 1,
                                           ConstraintMergeType.Xnor => MovementConstraints.Count(constraint => constraint.CanPerformMovement()) != 1,
                                           _ => throw new NotImplementedException(),
                                       };
}
