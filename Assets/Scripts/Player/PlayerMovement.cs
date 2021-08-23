using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using Unity.XR.GoogleVr;
using UnityEngine;

public class PlayerMovement : Transformer
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashDistance;
    [SerializeField] [Range(0, 1)] private float _dashStartAlign;
    [SerializeField] [Range(0, 1)] private float _dashEndAlign;
    [SerializeField] private float _dashCooldown;

    private Vector2 _movementAxes;
    private bool _dashReady = true;

    private int _freezeControls;
    public int FreezeControls
    {
        get => _freezeControls;
        set => _freezeControls = Mathf.Clamp(value, 0, Int32.MaxValue);
    }

    private void Awake()
    {
        Controls.InputMap.Gameplay.Enable();
        Controls.InputMap.Gameplay.Dash.performed += ctx => StartCoroutine(Dash());
    }

    private void FixedUpdate()
    {
        if (FreezeControls == 0)
            Move();
    }

    private void Move()
    {
        Vector2 inputAxes = Controls.InputMap.Gameplay.Move.ReadValue<Vector2>();
        _movementAxes = Vector2.Lerp(_movementAxes, inputAxes, _acceleration);
        _rigidbody.MovePosition(_rigidbody.position + _movementAxes * _speed);
    }

    private IEnumerator Dash()
    {
        Vector2 dashDirection = Controls.InputMap.Gameplay.Move.ReadValue<Vector2>();
        if (!_dashReady || dashDirection.magnitude <= 0)
            yield break;
        _dashReady = false;
        FreezeControls++;
        BezierCurve curve = new BezierCurve(new Vector3[4]);
        Vector2 targetPoint = (Vector2) transform.position + dashDirection * _dashDistance;
        curve.SetPoint(0, transform.position);
        curve.SetPoint(1, Vector2.Lerp(transform.position, targetPoint, _dashStartAlign));
        curve.SetPoint(2, Vector2.Lerp(transform.position, targetPoint, _dashEndAlign));
        curve.SetPoint(3, targetPoint);
        for (float i = 0; i < _dashDuration * 1.3f; i += Time.deltaTime)
        {
            _rigidbody.MovePosition(curve.Evaluate(i / _dashDuration));
            yield return null;
        }
        FreezeControls--;
        yield return new WaitForSeconds(_dashCooldown);
        _dashReady = true;
    }
}
