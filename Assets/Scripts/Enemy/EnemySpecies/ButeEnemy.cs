using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class ButeEnemy : Enemy
{
    [SerializeField] private float _period;
    [SerializeField] private int _projectiles;
    [SerializeField] private float _speed;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            for (int i = 0; i < _projectiles; i++)
            {
                Vector3 position = transform.position;
                position += (Vector3) VectorHelper.Rotate(Direction, ((float) i / _projectiles) * 360) * 20;
                ProjectileDirectionMovement newProjectile =
                    Instantiate(_projectilePrefab, position, Quaternion.identity).GetComponent<ProjectileDirectionMovement>();
                newProjectile.transform.parent = transform;
                newProjectile.Init(transform.position - position, _speed);
            }
            yield return new WaitForSeconds(_period);
        }
    }
}
