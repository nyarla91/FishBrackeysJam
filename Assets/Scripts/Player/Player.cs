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
    
    [SerializeField] private PlayerMeleeAttack _meleeAttack;
    public static PlayerMeleeAttack MeleeAttack => _instance._meleeAttack;
    
    [SerializeField] private PlayerRangedAttack _rangedAttack;
    public static PlayerRangedAttack RangedAttack => _instance._rangedAttack;
    
    [SerializeField] private PlayerInventory _inventory;
    public static PlayerInventory Inventory => _instance._inventory;
    
    public static Transform Transform => _instance.transform;

    private void Awake()
    {
        _instance = this;
    }
}
