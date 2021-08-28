﻿using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

[CreateAssetMenu(menuName = "Rod")]
public class RodInfo : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;
    
    [SerializeField] private string _displayName;
    public string DisplayName => _displayName;
    
    [SerializeField] private Sprite _spriteFull;
    public Sprite SpriteFull => _spriteFull;
    
    [SerializeField] private Sprite _spriteAttack;
    public Sprite SpriteAttack => _spriteAttack;

    [SerializeField] private Color _hookTint;
    public Color HookTint => _hookTint;

    [SerializeField] private int _cost;
    public int Cost => _cost;

    [SerializeField] private int _hookDamage;
    public int HookDamage => _hookDamage;

    [SerializeField] private int _meleeDamage;
    public int MeleeDamage => _meleeDamage;

    [SerializeField] private List<int> _effects;
    public List<int> Effects => _effects;

    [SerializeField] [TextArea(3, 10)] private string _description;
    public string PureDescription => _description;

    public string Description
    {
        get
        {
            string result = _description;
            for (int i = 0; i < _effects.Count; i++)
            {
                StringHelper.Replace(ref result, $"<X{i}>", _effects[i].ToString());
            }
            return result;
        }
    }

    public float GetEffectAsPercent(int index)
    {
        if (_effects.Count > index)
        {
            return (float) _effects[index] / 100;
        }
        return 1;
    }
}
