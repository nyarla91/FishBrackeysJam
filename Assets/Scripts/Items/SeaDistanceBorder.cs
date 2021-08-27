using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class SeaDistanceBorder : Transformer
{
    private static SeaDistanceBorder _instance;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public static bool PlayerClose { get; private set; }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        PlayerClose = Player.Transform.position.x > transform.position.x;
    }

    public static void ShowBorder()
    {
        _instance._spriteRenderer.enabled = true;
    }
}
