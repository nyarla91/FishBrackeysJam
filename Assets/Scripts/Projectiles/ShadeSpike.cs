using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeSpike : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _delay;
    [SerializeField] private float _raisedTime;

    private void Start()
    {
        StartCoroutine(Raising());
    }

    private IEnumerator Raising()
    {
        _collider.enabled = _animator.enabled = false;
        _spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(_delay);
        _spriteRenderer.color = Color.white;
        _collider.enabled = _animator.enabled = true;
        yield return new WaitForSeconds(_raisedTime);
        Destroy(gameObject);
    }
}
