using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;
using Random = UnityEngine.Random;

public class DunkeLoot : MonoBehaviour
{
    [SerializeField] private LootOnGround _lootOnGround;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _projectilesNumber;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _lootOnGround.OnLanded += Shoot;
    }

    private void Shoot()
    {
        float randomDegreeOffset = Random.Range(0, 359);
        for (int i = 0; i < _projectilesNumber; i++)
        {
            Vector2 direction = VectorHelper.DegreesToVector2((float) i / _projectilesNumber * 360 + randomDegreeOffset);
            ProjectileDirectionMovement newProjectile =
                Instantiate(_projectilePrefab, transform.position, Quaternion.identity)
                    .GetComponent<ProjectileDirectionMovement>();
            newProjectile.transform.parent = transform;
            newProjectile.Init(direction, _speed);
        }
    }
}
