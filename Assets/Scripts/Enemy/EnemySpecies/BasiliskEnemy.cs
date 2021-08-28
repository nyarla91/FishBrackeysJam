using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasiliskEnemy : Enemy
{
    [SerializeField] private int _projectilesPerLine;
    [SerializeField] private float _lineDelay;
    [SerializeField] private float _period;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _warningSpeed;
    [SerializeField] private float _speed;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(Random.Range(0, _cooldown));
        while (true)
        {
            Vector2 _lockOnDirection = Direction;
            ProjectileDirectionMovement newProjectile =
                Instantiate(_projectilePrefab, transform.position, Quaternion.identity)
                    .GetComponent<ProjectileDirectionMovement>();
            newProjectile.transform.parent = transform;
            newProjectile.Init(_lockOnDirection, _warningSpeed);
            yield return new WaitForSeconds(_lineDelay);
            for (int i = 0; i < _projectilesPerLine; i++)
            {
                newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity)
                        .GetComponent<ProjectileDirectionMovement>();
                newProjectile.Init(_lockOnDirection, _speed);
                yield return new WaitForSeconds(_period);
            }
            yield return new WaitForSeconds(_cooldown);
        }
    }
}
