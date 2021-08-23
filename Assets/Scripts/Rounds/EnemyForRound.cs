using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy For Round")]
public class EnemyForRound : ScriptableObject
{
    [SerializeField] private GameObject _enemy;
    public GameObject Enemy => _enemy;
    [SerializeField] private float _level;
    public float Level => _level;
}
