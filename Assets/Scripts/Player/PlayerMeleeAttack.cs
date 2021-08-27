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
        if (transform.localScale.x < 0)
        {
            overlapPosition *= new Vector2(-1, 1);
        }
        RodInHands.instance.HitStart(_cooldown, transform.localScale.x);
        Vector2 overlapBox = _attackBox;
        if (Rods.CurrentRod.Name.Equals("spearod"))
            overlapBox = new Vector2(Rods.CurrentRod.Effects[0], Rods.CurrentRod.Effects[1]);
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(overlapPosition, overlapBox, 0);
        foreach (var overlap in overlaps)
        {
            float damage = Rods.CurrentRodMeleeDamage;
            if (overlap.GetComponent<EnemyStatus>() != null)
                overlap.GetComponent<EnemyStatus>().TakeDamage(damage, true);
        }
        yield return new WaitForSeconds(_cooldown);
        _attackReady = true;
    }
}
