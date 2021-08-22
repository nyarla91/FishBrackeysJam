using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class Player : Transformer
{
    private static Player _instance;

    [SerializeField] private PlayerMovement _movement;
    public static PlayerMovement Movement => _instance._movement;
    
    public static Transform Transform => _instance.transform;

    private void Awake()
    {
        _instance = this;
    }
}
