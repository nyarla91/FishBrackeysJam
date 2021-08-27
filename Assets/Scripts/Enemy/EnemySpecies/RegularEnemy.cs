using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;
using Random = UnityEngine.Random;

public class RegularEnemy : Enemy
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shootPeriod;
    [SerializeField] private float _projectileSpeed;

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(Random.Range(0, _shootPeriod));
        while (true)
        {
            ProjectileDirectionMovement newProjectile =
                ProjectileLifecycle.Create<ProjectileDirectionMovement>(_projectilePrefab, transform.position);
            Vector2 direction = Player.Transform.position - transform.position;
            newProjectile.Init(direction, _projectileSpeed);
            yield return new WaitForSeconds(_shootPeriod);
        }
    }
}
