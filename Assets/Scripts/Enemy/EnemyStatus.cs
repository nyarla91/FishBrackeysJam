﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EnemyComponent
{
    [SerializeField] private float _healthMax;

    private float _health;
    public float Health
    {
        get => _health;
        private set
        {
            _health = value;
            Enemy.UI.SetHealth(Health / _healthMax);
        }
    }

    private void Start()
    {
        Health = _healthMax;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
