using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInInventory : Transformer
{
    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _ammountText;

    private ItemInfo _item;
    private int _ammount = 1;

    private int Ammount
    {
        get => _ammount;
        set
        {
            _ammount = value;
            if (_ammount > 1)
                _ammountText.text = _ammount.ToString();
            else
                _ammountText.text = "";
        }
    }

    public void Init(ItemInfo item)
    {
        Ammount = 1;
        _item = item;
        _sprite.sprite = _item.Sprite;
    }

    public void AddOne()
    {
        Ammount++;
    }
}
