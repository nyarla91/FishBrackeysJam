using System;
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

    public delegate void DeathHandler();
    public DeathHandler OnDeath;

    private void Start()
    {
        Health = _healthMax;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            if (OnDeath != null)
            {
                OnDeath();
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ProjectileDamage>() != null)
        {
            TakeDamage(other.gameObject.GetComponent<ProjectileDamage>().damage);
            Destroy(other.gameObject);
        }
    }
}
