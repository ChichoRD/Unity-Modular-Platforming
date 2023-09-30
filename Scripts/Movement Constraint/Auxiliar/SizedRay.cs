using System;
using UnityEngine;

public readonly struct SizedRay : IFormattable
{
    public readonly Ray ray;

    public readonly float Size { get; }
    public readonly Vector3 Origin => ray.origin;
    public readonly Vector3 Direction => ray.direction;

    public SizedRay(Vector3 origin, Vector3 displacement)
    {
        ray = new Ray(origin, displacement);
        Size = displacement.magnitude;
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return ((IFormattable)ray).ToString(format, formatProvider);
    }

    public static implicit operator Ray(SizedRay sizedRay)
    {
        return sizedRay.ray;
    }
}