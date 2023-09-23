using UnityEngine;

public class EuclideanSpeedMetric : MonoBehaviour, ISpeedMetric
{
    public float MeasureSpeed(Vector3 velocity) => velocity.magnitude;
}
