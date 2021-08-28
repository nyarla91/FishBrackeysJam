using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class ArcEnemy : Enemy
{
    [SerializeField] private int _projectilesAtOnce;
    [SerializeField] [Range (0, 360)] private float _arcDegree;
    [SerializeField] private float _shootPeriod;
    [SerializeField] private float _projectileSpeed;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(Random.Range(0, _shootPeriod));
        while (true)
        {
            for (float i = -_arcDegree * 0.5f; i <= _arcDegree * 0.5f; i += _arcDegree / (_projectilesAtOnce - 1))
            {
                ProjectileDirectionMovement newProjectile =
                    ProjectileLifecycle.Create<ProjectileDirectionMovement>(_projectilePrefab, transform.position);
                newProjectile.transform.parent = transform;
                Vector2 direction = VectorHelper.Rotate(Direction, i);
                newProjectile.Init(direction, _projectileSpeed);
            }
            yield return new WaitForSeconds(_shootPeriod);
        }
    }
}
