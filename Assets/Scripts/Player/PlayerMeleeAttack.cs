using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private GameObject _slashPrefab;
    [SerializeField] private Vector2 _attackBox;
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldown;

    private bool _attackReady = true;
    
    private void Awake()
    {
        Controls.InputMap.Gameplay.MeleeAttack.performed += ctx => StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (!_attackReady)
            yield break;
        _attackReady = false;
        Vector2 overlapPosition = (Vector2) transform.position + new Vector2(_attackBox.x * 0.5f, 0);
        Transform slash = Instantiate(_slashPrefab, overlapPosition, Quaternion.identity).transform;
        slash.localScale = _attackBox;
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(overlapPosition, _attackBox, 0);
        foreach (var overlap in overlaps)
        {
            if (overlap.GetComponent<EnemyStatus>() != null)
                overlap.GetComponent<EnemyStatus>().TakeDamage(_damage);
        }
        yield return new WaitForSeconds(_cooldown);
        Destroy(slash.gameObject);
        _attackReady = true;
    }
}
