using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials.Sound;
using UnityEngine;

public class Rods : MonoBehaviour
{
    private static Rods _instance;
    [SerializeField] private RodInfo _currentRod;
    [SerializeField] private ItemInfo _ritualRodItem;

    private static int _ritualKills;

    public static int RitualKills
    {
        get => _ritualKills;
        set
        {
            _ritualKills = value;
            print(_ritualKills);
            if (_ritualKills == CurrentRod.Effects[0])
            {
                SoundPlayer.Play("ritual", 1);
                Items.AddItem(_instance._ritualRodItem);
                Player.UI.AddItem(_instance._ritualRodItem);
                _ritualKills = Int32.MinValue;
            }
        }
    }

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

    public static void Reset()
    {
        _ritualKills = 0;
        _currentRodStatsMultiplier = 1;
    }
}
