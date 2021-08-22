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
    
    [SerializeField] private PlayerUI _ui;
    public static PlayerUI UI => _instance._ui;
    
    [SerializeField] private PlayerStatus _status;
    public static PlayerStatus Status => _instance._status;
    
    public static Transform Transform => _instance.transform;

    private void Awake()
    {
        _instance = this;
    }
}
