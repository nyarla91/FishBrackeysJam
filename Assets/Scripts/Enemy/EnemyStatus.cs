using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EnemyComponent
{
    [SerializeField] private float _healthMax;

    private float _health;
    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            Enemy.UI.SetHealth(_health / _healthMax);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
