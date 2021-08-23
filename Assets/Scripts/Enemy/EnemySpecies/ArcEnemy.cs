﻿using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class ArcEnemy : Enemy
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _projectilesAtOnce;
    [SerializeField] [Range (0, 360)] private float _arcDegree;
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
            for (float i = -_arcDegree * 0.5f; i <= _arcDegree * 0.5f; i += _arcDegree / (_projectilesAtOnce - 1))
            {
                ProjectileDirectionMovement newProjectile =
                    ProjectileLifecycle.Create<ProjectileDirectionMovement>(_projectilePrefab, transform.position);
                Vector2 direction = VectorHelper.Rotate(Player.Transform.position - transform.position, i);
                newProjectile.Init(direction, _projectileSpeed);
            }
            yield return new WaitForSeconds(_shootPeriod);
        }
    }
}