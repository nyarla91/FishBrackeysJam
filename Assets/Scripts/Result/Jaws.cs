using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class Jaws : Transformer
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _eatSprite;

    private Vector3 _origin;

    private void Awake()
    {
        _origin = transform.position;
    }

    private void Start()
    {
        StartCoroutine(Eat());
    }

    private IEnumerator Eat()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime * 0.5f)
        {
            transform.position = _origin + new Vector3(0, (-1 + i) * 3, 0);
            _spriteRenderer.color = new Color(1, 1, 1, i);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        _spriteRenderer.sprite = _eatSprite;
        transform.position = _origin + new Vector3(0.8f, 0, 0);
    }
}
