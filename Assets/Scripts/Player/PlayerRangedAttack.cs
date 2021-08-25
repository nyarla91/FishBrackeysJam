using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject _hookPrefab;
    [SerializeField] private float _hookSpeed;
    [SerializeField] private float _cooldown;

    private bool _attackReady = true;

    private void Awake()
    {
        Controls.InputMap.Gameplay.RangedAttack.performed += ctx => StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (!_attackReady)
            yield break;
        _attackReady = false;
        Player.Movement.FreezeControls++;

        Vector2 direction = CameraProperties.MousePosition2D - transform.position;
        ProjectileDirectionMovement newProjectile =
            Instantiate(_hookPrefab, transform.position, Quaternion.identity).GetComponent<ProjectileDirectionMovement>();
        
        newProjectile.Init(direction, _hookSpeed * Items.GetEffectAsPercent("hook_speed"));
        ProjectileLifecycle projectileLifecycle = newProjectile.GetComponent<ProjectileLifecycle>();
        bool destroyed = false;
        projectileLifecycle.OnProjectileDestroy += () => ProjectileDestroyed(ref destroyed);
        newProjectile.GetComponent<ProjectileDamage>().damage = Rods.CurrentRodHookDamage * Items.GetEffectAsPercent("hook_damage");
        yield return new WaitUntil(() => destroyed);
        Player.Movement.FreezeControls--;

        yield return new WaitForSeconds(_cooldown);
        _attackReady = true;
    }

    private void ProjectileDestroyed(ref bool destroyed)
    {
        destroyed = true;
    }
}
