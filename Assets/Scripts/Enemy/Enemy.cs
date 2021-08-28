using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class Enemy : Transformer
{
    [SerializeField] protected EnemyStatus _status;
    [SerializeField] protected GameObject _projectilePrefab;
    public EnemyStatus Status => _status;
    [SerializeField] private EnemyUI _ui;
    public EnemyUI UI => _ui;

    protected Vector2 Direction => ((Vector2) Player.Transform.position - (Vector2) transform.position).normalized;
}
