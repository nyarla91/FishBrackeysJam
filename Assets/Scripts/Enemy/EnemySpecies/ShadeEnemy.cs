using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeEnemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Dictionary<string, IEnumerator> _states = new Dictionary<string, IEnumerator>();

    [Header("Attack")]
    [SerializeField] private float _period;

    private void Awake()
    {
        _states.Add("spikes", SpikesAttack());
    }

    private void NextAttack()
    {
        
    }

    private IEnumerator SpikesAttack()
    {
        yield break;
    }
}
