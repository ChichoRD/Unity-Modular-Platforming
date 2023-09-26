using System;
using UnityEngine;

[CreateAssetMenu(fileName = NAME, menuName = PATH)]
public class SecondsDurationProfileObject : ScriptableObject, IDurationProvider
{
    private const string NAME = "Seconds Duration Profile";
    private const string PATH = "Movement Profiles/" + NAME;

    [SerializeField] private float duration = 0.2f;
    public TimeSpan GetDuration() => TimeSpan.FromSeconds(duration);
}