using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private static CameraControl _instance;

    private Vector2 targetPoint;
    public static Vector2 TargetPoint
    {
        get => _instance.targetPoint;
        set => _instance.targetPoint = value;
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        transform.position = VectorHelper.SetZ(Vector2.Lerp(transform.position, targetPoint, Time.deltaTime * 10), -100);
    }
}
