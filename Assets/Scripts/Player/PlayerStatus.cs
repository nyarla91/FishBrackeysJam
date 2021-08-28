using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float _healthMax;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Sprite _deadSprite;
    [SerializeField] private GameObject _jawsPrefab;
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

    public float HealthPercent => Health / HealthMax;
    public bool HealthFellBelowHalf { get; set; }

    private float _immunity;

    private void Start()
    {
        Health = _healthMax;
    }

    private void Update()
    {
        if (_immunity > 0)
        {
            _immunity -= Time.deltaTime;
            _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, MathHelper.TimeSin(0.2f, 0.5f, 16));
        }
        else
        {
            _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, 1);
        }
    }

    public void TakeDamage(float damage)
    {
        if (_immunity <= 0)
        {
            float totalBonus = 1;
            
            // Bonus calculation
            if (SeaDistanceBorder.PlayerClose)
                totalBonus -= Items.GetEffectAsPercent("close_defence");
            else
                totalBonus -= Items.GetEffectAsPercent("far_defence");
            
            damage *= totalBonus;
            if (Items.GetEffect("triple") > 0)
                damage *= 3;

            float healthPercentBeforeDamage = HealthPercent;
            Health -= damage;
            if (Health > 0)
            {
                if (healthPercentBeforeDamage >= 0.5f && HealthPercent < 0.5f)
                    HealthFellBelowHalf = true;
                StopAllCoroutines();
                StartCoroutine(TurnRedOnDamage());
                Immunity(0.5f + Items.GetEffect("iframe"));
            }
            else
            {
                Health = 0;
                StopAllCoroutines();
                StartCoroutine(Die());
            }
        }
    }

    public void RestoreHealth(float healthRestored)
    {
        if (Rods.CurrentRod.Name.Equals("no_heal"))
            return;
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

    public void Immunity(float duration)
    {
        if (_immunity < duration && Items.GetEffect("no_immune") == 0)
            _immunity = duration;
    }

    private IEnumerator TurnRedOnDamage()
    {
        float newGB = 0;
        Player.SpriteRenderer.color = new Color(1, newGB, newGB, Player.SpriteRenderer.color.a);
        while (Player.SpriteRenderer.color.g < 1)
        {
            yield return null;
            newGB = Mathf.Lerp(Player.SpriteRenderer.color.g, 1f, 5 * Time.deltaTime);
            Player.SpriteRenderer.color = new Color(1, newGB, newGB, Player.SpriteRenderer.color.a);
        }
    }

    private IEnumerator Die()
    {
        Player.Movement.StopAllCoroutines();
        Destroy(Map.transform.gameObject);
        Player.UI.Hide();
        RodInHands.instance.Hide();
        RodInHands.instance.attacking = true;
        Player.Movement.FreezeControls = 100;
        _animator.enabled = false;
        _spriteRenderer.sprite = _deadSprite;
        yield return new WaitForSeconds(1);
        Instantiate(_jawsPrefab, transform.position + Vector3.back, Quaternion.identity);
        yield return new WaitForSeconds(4);
        Player.UI.Show();
        Result.Show(false);
        yield break;
    }
}
