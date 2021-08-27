using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyStatus : EnemyComponent
{
    [SerializeField] private float _healthMax;
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private FishInfo _loot;

    [SerializeField] private float _health;
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

    public void TakeDamage(float damage, bool melee)
    {
        float totalBonus = 1;
        
        //Bonus calculation
        if (Rods.CurrentRod.Name.Equals("berserker"))
            totalBonus += 1 - Player.Status.HealthPercent;
        if (melee)
        {
            totalBonus += Items.GetEffectAsPercent("melee_damage");
        }
        else
        {
            totalBonus += Items.GetEffectAsPercent("hook_damage");
        }
        totalBonus += Items.GetEffectAsPercent("no_immune");
        damage *= totalBonus;
        if (Items.GetEffect("triple") > 0)
            damage *= 3;
        
        Health -= damage;
        if (Rods.CurrentRod.Name.Equals("vampire"))
        {
            Player.Status.RestoreHealth(Rods.CurrentRod.GetEffectAsPercent(0) * damage);
        }
        if (Health <= 0) Die();
    }

    private void Die()
    {
        if (OnDeath != null) OnDeath();

        Rounds.instance.enemies.Remove(gameObject);
        LootOnGround newLoot = Instantiate(_lootPrefab, transform.position, Quaternion.identity).GetComponent<LootOnGround>();
        newLoot.Init(LootArea.RandomPoint(), _loot);
        if (Rods.CurrentRod.Name.Equals("scraple"))
        {
            Player.Status.Immunity(Rods.CurrentRod.Effects[0]);
        }
        else if (Rods.CurrentRod.Name.Equals("ritual"))
        {
            Rods.RitualKills++;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ProjectileLifecycle>() != null)
        {
            TakeDamage(other.gameObject.GetComponent<ProjectileDamage>().damage, false);
            Destroy(other.gameObject);
        }
    }
}
