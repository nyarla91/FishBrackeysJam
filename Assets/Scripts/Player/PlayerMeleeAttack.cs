using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private Vector2 _attackBox;
    [SerializeField] private float _cooldown;

    private bool _attackReady = true;
    
    private void Awake()
    {
        Controls.InputMap.Gameplay.MeleeAttack.performed += ctx => StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (!_attackReady || RodInHands.instance.attacking)
            yield break;
        _attackReady = false;
        Vector2 overlapPosition = (Vector2) transform.position + new Vector2(_attackBox.x * 0.5f, 0);
        RodInHands.instance.HitStart(_cooldown);
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(overlapPosition, _attackBox, 0);
        foreach (var overlap in overlaps)
        {
            float damage = Rods.CurrentRodMeleeDamage * Items.GetEffectAsPercent("melee_damage");
            if (overlap.GetComponent<EnemyStatus>() != null)
                overlap.GetComponent<EnemyStatus>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(_cooldown);
        _attackReady = true;
    }
}
