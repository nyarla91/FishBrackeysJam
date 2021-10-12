using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _ability;
    [SerializeField] private Image _sprite;
    [SerializeField] private GameObject _soldOutOverlay;

    private ItemInfo _item;
    private bool _sold;

    private bool Sold
    {
        get => _sold;
        set
        {
            _sold = value;
            if (!Permanent)
                _soldOutOverlay.SetActive(value);
        }
    }

    private bool Permanent => _soldOutOverlay == null;

    private int _countedCost;

    public void Init(ItemInfo item)
    {
        Sold = false;
        _item = item;
        _name.text = _item.DisplayName;
        _countedCost = Mathf.RoundToInt(_item.Cost * Rounds.CostMultiplier);
        _cost.text = $"{_countedCost} $";
        _ability.text = _item.DisplayAbility;
        _sprite.sprite = _item.Sprite;
    }

    public void Buy()
    {
        if (Shop.Money >= _countedCost)
        {
            SoundPlayer.Play("buy", 1);
            Shop.Money -= _countedCost;
            Sold = true;
            Player.UI.AddItem(_item);
            Items.AddItem(_item);
        }
    }
}
