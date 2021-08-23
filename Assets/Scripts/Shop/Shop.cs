using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using TMPro;
using UnityEngine;

public class Shop : Transformer
{
    private static Shop _instance;

    [SerializeField] private ShopTab _startingTab;
    
    private float targetX = -1920;

    private void Awake()
    {
        _instance = this;
        Show();
    }

    private void FixedUpdate()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(targetX, 0), 0.25f);
    }

    public static void Show()
    {
        _instance.targetX = 0;
        _instance._startingTab.Open();
    }
    
    public static void Hide()
    {
        _instance.targetX = -2000;
    }
}
