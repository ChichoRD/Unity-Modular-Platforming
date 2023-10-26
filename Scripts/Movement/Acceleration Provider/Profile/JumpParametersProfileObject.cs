using System;
using UnityEngine;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class JumpParametersProfileObject : ScriptableObject, ISpeedProvider, IAccerlerationProvider, IDurationProvider
{
    private const string NAME = "Jump Parameters Profile";
    private const string PATH = "Movement Profiles/" + NAME;

    [SerializeField] private float _peakJumpHeight;
    [SerializeField] private float _timeToPeakJumpHeight;

    /* Kiematics:
     * v = v0 + a * dt
     * h = h0 + v0 * dt + 0.5 * a * dt^2
     * 
     * To reach peak height:
     * v = 0
     * v0 = -a * dt
     * 
     * dh = -a * dt + 0.5 * a * dt^2
     * dh = (-a + 0.5 * a * dt) * dt
     * dh = -0.5f * a * dt^2
     * 
     * a = -2 * dh / dt^2
     * v0 = 2 * dh / dt
     */

    public float GetSpeed() => 2.0f * _peakJumpHeight / _timeToPeakJumpHeight;
    public float GetAccelerationMagnitude() => -2.0f * _peakJumpHeight / (_timeToPeakJumpHeight * _timeToPeakJumpHeight);
    public TimeSpan GetDuration() => TimeSpan.FromSeconds(_timeToPeakJumpHeight);
}