using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

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
        while (true)
        {
            ProjectileDirectionMovement newProjectile =
                Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileDirectionMovement>();
            Vector2 direction = Player.Transform.position - transform.position;
            newProjectile.Init(direction, _projectileSpeed);
            yield return new WaitForSeconds(_shootPeriod);
        }
    }
}
