﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Loot")]
public class FishInfo : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private Vector2 _spriteScale = new Vector2(120,120);
    public Vector2 SpriteScale => _spriteScale;

    [SerializeField] private int _healthRestored;
    public int HealthRestored => _healthRestored;

    [SerializeField] private int _cost;
    public int Cost => _cost;
}