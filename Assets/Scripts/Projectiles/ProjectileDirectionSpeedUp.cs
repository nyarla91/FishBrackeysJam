using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDirectionSpeedUp : ProjectileDirectionMovement
{
    [SerializeField] private float _acceleration;
    private void Update()
    {
        _speed += _acceleration / 50;
    }
}
