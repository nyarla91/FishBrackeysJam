using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class PlayerMovement : Transformer
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;

    private Vector2 _movementAxes;

    private void Awake()
    {
        Controls.InputMap.Gameplay.Enable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputAxes = Controls.InputMap.Gameplay.Move.ReadValue<Vector2>();
        _movementAxes = Vector2.Lerp(_movementAxes, inputAxes, _acceleration);
        _rigidbody.MovePosition(_rigidbody.position + _movementAxes * _speed);
    }
}
