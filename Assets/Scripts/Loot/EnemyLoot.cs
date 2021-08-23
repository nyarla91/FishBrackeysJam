using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Loot")]
public class EnemyLoot : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private float _spriteScale;
    public float SpriteScale => _spriteScale;

    [SerializeField] private int _healthRestored;
    public int HealthRestored => _healthRestored;

    [SerializeField] private int _cost;
    public int Cost => _cost;
}
