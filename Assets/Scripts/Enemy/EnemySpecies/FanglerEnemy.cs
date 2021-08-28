using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FanglerEnemy : Enemy
{
    [SerializeField] private float _period;
    [SerializeField] private int _projectilesPerLine;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _speed;

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(Random.Range(0, _cooldown));
        while (true)
        {
            for (int i = 0; i < _projectilesPerLine; i++)
            {
                yield return new WaitForSeconds(_period);
                ProjectileDirectionMovement newProjectile =
                    Instantiate(_projectilePrefab, transform.position, Quaternion.identity)
                        .GetComponent<ProjectileDirectionMovement>();
                newProjectile.Init(Direction, _speed);
            }
            yield return new WaitForSeconds(_cooldown);
        }
    }
}
