using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStatus _status;
    public EnemyStatus Status => _status;
    [SerializeField] private EnemyUI _ui;
    public EnemyUI UI => _ui;
}
