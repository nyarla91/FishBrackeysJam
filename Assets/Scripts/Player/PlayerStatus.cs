using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float _healthMax;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public float HealthMax
    {
        get => _healthMax;
        set
        {
            _healthMax = value;
            Player.UI.UpdateHealth(_health, _healthMax);
        }
    }

    private float _health;
    public float Health
    {
        get => _health;
        private set
        {
            _health = value;
            Player.UI.UpdateHealth(_health, _healthMax);
        }
    }

    private bool _immune;

    private void Start()
    {
        Health = _healthMax;

    }

    public void TakeDamage(float damage)
    {
        if (!_immune)
        {
            Health -= damage;
            StartCoroutine(DamageInvulnerability(0.5f + Items.GetEffect("iframe")));
        }
    }

    public void RestoreHealth(float healthRestored)
    {
        print(healthRestored);
        Health = Mathf.Clamp(Health + healthRestored, 0, _healthMax);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ProjectileDamage>() != null)
        {
            TakeDamage(other.gameObject.GetComponent<ProjectileDamage>().damage);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator DamageInvulnerability(float duration)
    {
        _immune = true;
        _spriteRenderer.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(duration);
        _immune = false;
        _spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
