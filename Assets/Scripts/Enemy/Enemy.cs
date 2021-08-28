using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class Enemy : Transformer
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected EnemyStatus _status;
    [SerializeField] protected GameObject _projectilePrefab;
    public EnemyStatus Status => _status;
    [SerializeField] private EnemyUI _ui;
    public EnemyUI UI => _ui;

    protected Vector2 Direction => ((Vector2) Player.Transform.position - (Vector2) transform.position).normalized;

    protected virtual void Start()
    {
        StartCoroutine(BlendIn());
    }

    private IEnumerator BlendIn()
    {
        _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, 0);
        for (float i = 0; i <= 1; i += Time.deltaTime * 2)
        {
            _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, i);
            yield return null;
        }
    }
}
