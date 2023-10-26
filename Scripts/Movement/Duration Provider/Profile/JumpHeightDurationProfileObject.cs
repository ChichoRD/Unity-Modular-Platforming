using UnityEngine;
using System;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class JumpHeightDurationProfileObject : ScriptableObject, IDurationProvider, ISpeedProvider
{
    private const string NAME = "Jump Height Duration Profile";
    private const string PATH = "Movement Profiles/" + NAME;

    [RequireInterface(typeof(ISpeedProvider),typeof(ScriptableObject))]
    [SerializeField]
    private Object _jumpSpeedProviderObject = null;
    private ISpeedProvider JumpSpeedProvider => _jumpSpeedProviderObject as ISpeedProvider;

    [RequireInterface(typeof(IDurationProvider))]
    [SerializeField]
    private Object _jumpCancelDurationProviderObject = null;
    private IDurationProvider JumpCancelDurationProvider => _jumpCancelDurationProviderObject as IDurationProvider;

    [SerializeField]
    private float _minimumJumpHeight;

    /* Kiematics:
     * dh = v0 * dt + 0.5 * a * dt^2
     * dv = a * dt
     * 
     * dh = v0 * dt + 0.5 * dv * dt
     * dh = (v0 + 0.5 * dv) * dt
     * 
     * dt = dh / (v0 + 0.5 * dv)
     * v0 = 0
     * 
     * dt = dh / (0.5 * dv)
     * dt = 2.0 * dh / dv
     */

    public TimeSpan GetDuration() => TimeSpan.FromSeconds(Mathf.Max(
                                                            2.0f * _minimumJumpHeight / JumpSpeedProvider.GetSpeed() -
                                                                (float)(JumpCancelDurationProvider?.GetDuration().TotalSeconds ?? 0.0f),
                                                            0.0f));

    public float GetSpeed() => _minimumJumpHeight / (Mathf.Max((float)GetDuration().TotalSeconds, (float)(JumpCancelDurationProvider?.GetDuration().TotalSeconds ?? 0.0f)) + float.Epsilon);
}