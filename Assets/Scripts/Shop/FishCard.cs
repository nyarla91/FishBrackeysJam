using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishCard : Transformer
{
    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _eatText;
    [SerializeField] private TextMeshProUGUI _sellText;
    [SerializeField] private TextMeshProUGUI _ammountText;

    private FishInfo _fish;
    public FishInfo Fish => _fish;
    
    private float _targetX;
    public float TargetX
    {
        get => _targetX;
        set => _targetX = value;
    }
    
    private int _ammount;
    public int Ammount
    {
        get => _ammount;
        set
        { 
            _ammount = value;
            if (_ammount > 1)
            {
                _ammountText.text = $"x{_ammount}";
            }
            else if (_ammount == 1)
            {
                _ammountText.text = "";
            }
            else
            {
                FishWindow.RemoveCard(_fish);
                Destroy(gameObject);
            }
        }
    }

    public void Init(FishInfo fish, int newAmmount)
    {
        Ammount = newAmmount;
        _fish = fish;
        _sprite.sprite = _fish.Sprite;
        _eatText.text = $"Eat ({_fish.HealthRestored * (1 + Items.GetEffectAsPercent("fish_heal"))} HP)";
        _sellText.text = $"Sell ({_fish.Cost} $)";
        TargetX = rectTransform.anchoredPosition.x;
    }

    private void FixedUpdate()
    {
        rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(rectTransform.anchoredPosition.x, TargetX, 0.3f), 0);
    }

    public void Eat()
    {
        Player.Status.RestoreHealth(_fish.HealthRestored * (1 + Items.GetEffectAsPercent("fish_heal")));
        Spend();
    }

    public void Sell()
    {
        Shop.Money += _fish.Cost;
        Result.moneyEarned += _fish.Cost;
        Spend();
    }

    private void Spend()
    {
        Player.Inventory.fish.Remove(_fish);
        Ammount--;
    }
}
