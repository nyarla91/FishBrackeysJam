﻿using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class Items : MonoBehaviour
{
    private static Dictionary<string, int> _itemEffects = new Dictionary<string, int>();
    private static List<ItemInfo> _items = new List<ItemInfo>();

    public static void AddItem(ItemInfo itemToAdd)
    {
        _items.Add(itemToAdd);
        if (_itemEffects.ContainsKey(itemToAdd.Name))
        {
            _itemEffects[itemToAdd.Name] += itemToAdd.Effect;
        }
        else
        {
            _itemEffects.Add(itemToAdd.Name, itemToAdd.Effect);
        }
        PerformOneTimeBuyEffect(itemToAdd);
    }

    public static int GetEffect(string effectName)
    {
        if (_itemEffects.ContainsKey(effectName))
        {
            return _itemEffects[effectName];
        }
        return 0;
    }

    public static float GetEffectAsPercent(string effectName)
    {
        if (_itemEffects.ContainsKey(effectName))
        {
            return (float) _itemEffects[effectName] / 100;
        }
        return 0;
    }

    public static void PerformOneTimeBuyEffect(ItemInfo item)
    {
        switch (item.Name)
        {
            case "health":
            {
                Player.Status.HealthMax += item.Effect;
                Player.Status.RestoreHealth(item.Effect);
                break;
            }
            case "fish_heal":
            {
                FishWindow.ReinitFish();
                break;
            }
            case "far_defence":
            {
                SeaDistanceBorder.ShowBorder();
                break;
            }
            case "close_defence":
            {
                SeaDistanceBorder.ShowBorder();
                break;
            }
        }
    }
}
