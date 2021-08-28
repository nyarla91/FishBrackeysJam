using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileWaveMovement : ProjectileMovement
{
    [SerializeField] private float _speed;
    [SerializeField] private float _waveSpeed;
    [SerializeField] private float _waveAmpletude;
    
    private Vector2 _direction;
    private Vector2 _normal;
    private float _startTime;
    private Vector2 _origin;
    
    public void Init(Vector2 direction)
    {
        _direction = direction;
        _normal = VectorHelper.Rotate(_direction, Random.value < 0.5f ? 90 : -90);
        _startTime = Time.time;
        _origin = transform.position;
    }

    private void Update()
    {
        Vector3 newPosition = _origin + _speed * (Time.time - _startTime) * _direction;
        Vector3 waveOffset =  _waveAmpletude * Mathf.Sin((Time.time - _startTime) * _waveSpeed) * _normal;
        transform.position = VectorHelper.SetZ(newPosition + waveOffset, -1);
    }
}
