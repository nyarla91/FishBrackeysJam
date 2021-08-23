using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Round")]
public class Round : ScriptableObject
{
    [SerializeField] private float _duration;
    public float Duration => _duration;

    [SerializeField] private float _levelModifier;
    public float LevelModifier => _levelModifier;
    
    [SerializeField] private List<EnemyForRound> _enemies;
    public List<EnemyForRound> Enemies => _enemies;
}
