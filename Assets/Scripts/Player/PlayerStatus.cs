using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float _healthMax;

    private float _health;

    public float Health
    {
        get => _health;
        private set
        {
            _health = value;
            Player.UI.UpdateHealth(_health / _healthMax);
        }
    }

    private void Start()
    {
        Health = _healthMax / 7;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void RestoreHealth(float healthRestored)
    {
        print(healthRestored);
        Health = Mathf.Clamp(Health + healthRestored, 0, _healthMax);
        print(Health);
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
