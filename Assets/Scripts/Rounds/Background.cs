using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private static Background _instance;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _instance = this;
    }

    public static void SetBackground(Sprite background)
    {
        _instance._spriteRenderer.sprite = background;
    }
}
