using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProjectileDirectionMovement : ProjectileMovement
{
    private Vector2 _direction;
    private float _speed;

    public void Init(Vector2 direction, float speed)
    {
        _direction = direction.normalized;
        _speed = speed;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _speed * _direction);
    }
}
