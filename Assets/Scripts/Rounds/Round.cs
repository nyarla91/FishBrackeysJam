using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Round")]
public class Round : ScriptableObject
{
    [SerializeField] private List<int> _waves;
    public List<int> Waves => _waves;
    
    [SerializeField] private List<EnemyForRound> _enemies;
    public List<EnemyForRound> Enemies => _enemies;
}
