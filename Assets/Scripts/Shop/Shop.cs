using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using TMPro;
using UnityEngine;

public class Shop : Transformer
{
    private static Shop _instance;

    [SerializeField] private FishWindow _fishWindow;
    [SerializeField] private ShopTab _startingTab;
    [SerializeField] private TextMeshProUGUI _moneyCounter;
    [SerializeField] private List<Attention> _attentions;
    
    private List<IShopWindow> _windows = new List<IShopWindow>();
    private float targetX = -1920;

    private int _money;

    public static int Money
    {
        get => _instance._money;
        set
        {
            _instance._money = value;
            _instance._moneyCounter.text = $"{value} $";
        }
    }

    public static bool Open => _instance.targetX > -1900;

    private void Awake()
    {
        _instance = this;
        Money = 0;
    }

    private void FixedUpdate()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(targetX, 0), 0.15f);
    }

    public static void AddIShopWindow(IShopWindow added)
    {
        _instance._windows.Add(added);
    }

    public static void GenerateContent()
    {
        foreach (var attention in _instance._attentions)
        {
            attention.Show();
        }
        foreach (var window in _instance._windows)
        {
            window.GenerateContent();
        }
    }

    public static void Show()
    {
        _instance._fishWindow.GenerateContent();
        _instance.targetX = 0;
        _instance._startingTab.Open();
        Player.Movement.FreezeControls++;
        Music.instance.Play(Music.instance.ShopTheme);
    }
    
    public static void Hide()
    {
        _instance.targetX = -2000;
        Player.Movement.FreezeControls--;
        NextRoundArea.readyToReturn = true;
        Background.SetBackground(Rounds.NextRound.Biome);
        Music.instance.Stop();
    }

    public void Close() => Hide();
}
