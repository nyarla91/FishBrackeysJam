using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject _hookPrefab;
    [SerializeField] private GameObject _shotrodBulletPrefab;
    [SerializeField] private float _hookSpeed;
    [SerializeField] private float _cooldown;

    private bool _attackReady = true;

    private void Awake()
    {
        Controls.InputMap.Gameplay.RangedAttack.performed += ctx => StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (!_attackReady || RodInHands.instance.attacking)
            yield break;
        _attackReady = false;
        if (!Rods.CurrentRod.Name.Equals("spinning"))
            Player.Movement.FreezeControls++;

        Vector2 direction = CameraProperties.MousePosition2D - transform.position;
        transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        
        ProjectileDirectionMovement newProjectile =
            Instantiate(_hookPrefab, transform.position, Quaternion.identity).GetComponent<ProjectileDirectionMovement>();
        float speed = 0;
        if (!Rods.CurrentRod.Name.Equals("bow"))
        {
            float totalSpeedBonus = 1;
            totalSpeedBonus += Items.GetEffectAsPercent("hook_speed");
            if (Player.Status.HealthFellBelowHalf)
                totalSpeedBonus += Items.GetEffectAsPercent("half_health_hook_speed");
            speed = _hookSpeed * totalSpeedBonus;
        }
        else
            speed = _hookSpeed * Rods.CurrentRod.GetEffectAsPercent(0);
        newProjectile.Init(direction, speed);
        ProjectileLifecycle projectileLifecycle = newProjectile.GetComponent<ProjectileLifecycle>();
        bool destroyed = false;
        projectileLifecycle.OnProjectileDestroy += () => ProjectileDestroyed(ref destroyed);
        newProjectile.GetComponent<ProjectileDamage>().damage = Rods.CurrentRodHookDamage;
        RodInHands.instance.ThrowStart(projectileLifecycle, _cooldown, direction);

        if (Rods.CurrentRod.Name.Equals("shotrod"))
        {
            for (int i = 0; i < Rods.CurrentRod.Effects[0]; i++)
            {
                float t = MathHelper.Evaluate(i, 0, Rods.CurrentRod.Effects[0] - 1);
                float degree = Mathf.Lerp(-Rods.CurrentRod.Effects[2] / 2, Rods.CurrentRod.Effects[2] / 2, t);
                ProjectileDirectionMovement newBullet =
                    Instantiate(_shotrodBulletPrefab, transform.position, Quaternion.identity).GetComponent<ProjectileDirectionMovement>();
                Vector2 bulletDirection = VectorHelper.Rotate(direction, degree);
                newBullet.Init(bulletDirection, 0.3f);
                newBullet.GetComponent<ProjectileDamage>().damage = Rods.CurrentRodHookDamage * Rods.CurrentRod.GetEffectAsPercent(1);
            }
        }
        
        yield return new WaitUntil(() => destroyed);
        if (!Rods.CurrentRod.Name.Equals("spinning"))
            Player.Movement.FreezeControls--;

        yield return new WaitForSeconds(_cooldown);
        _attackReady = true;
    }

    private void ProjectileDestroyed(ref bool destroyed)
    {
        destroyed = true;
    }
}
