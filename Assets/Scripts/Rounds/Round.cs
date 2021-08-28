using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Round")]
public class Round : ScriptableObject
{
    [SerializeField] private Sprite _biome;
    public Sprite Biome => _biome;
    
    [Header("Combat")]
    [SerializeField] private List<int> _waves;
    public List<int> Waves => _waves;
    
    [SerializeField] private List<EnemyForRound> _enemies;
    public List<EnemyForRound> Enemies => _enemies;
    
    [Header("Shop")]
    [SerializeField] private float _costMultiplier = 1;
    public float CostMultiplier => _costMultiplier;
    
    [SerializeField] private float _rodStatsMultiplier = 1;
    public float RodStatsMultiplier => _rodStatsMultiplier;
}
