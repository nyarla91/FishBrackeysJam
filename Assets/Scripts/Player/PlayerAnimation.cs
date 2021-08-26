using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public const string Idle = "Idle";
    public const string Walk = "Walk";
    public const string Dash = "Dash";
    public const string DashEnd = "DashEnd";
    public const string DashStart = "DashStart";
    
    [SerializeField] private Animator _animator;

    public void Play(string state)
    {
        _animator.Play("Player" + state);
    }
}
