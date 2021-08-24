using System.Collections;
using System.Collections.Generic;
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

    public void Init(ItemInfo item)
    {
        Sold = false;
        _item = item;
        _name.text = _item.DisplayName;
        _cost.text = $"{_item.Cost} $";
        _ability.text = _item.DisplayAbility;
        _sprite.sprite = _item.Sprite;
    }

    public void Buy()
    {
        if (Shop.Money >= _item.Cost)
        {
            Shop.Money -= _item.Cost;
            Sold = true;
            Player.UI.AddItem(_item);
            Items.AddItem(_item);
        }
    }
}
