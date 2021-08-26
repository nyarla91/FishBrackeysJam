using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rods : MonoBehaviour
{
    private static Rods _instance;
    [SerializeField] private RodInfo _currentRod;

    public static RodInfo CurrentRod
    {
        get => _instance._currentRod;
        set
        {
            _instance._currentRod = value;
        }
    }

    private static float _currentRodStatsMultiplier = 1;

    public static float CurrentRodStatsMultiplier
    {
        get => _currentRodStatsMultiplier;
        set => _currentRodStatsMultiplier = value;
    }
    
    public static int CurrentRodHookDamage => Mathf.RoundToInt(CurrentRod.HookDamage * CurrentRodStatsMultiplier);
    public static int CurrentRodMeleeDamage => Mathf.RoundToInt(CurrentRod.MeleeDamage * CurrentRodStatsMultiplier);

    private void Awake()
    {
        _instance = this;
    }
}
