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

    public void ShowTooltip()
    {
        string ability = _item.PureDisplayAbility;
        string newEffect = $"{_item.Effect}";
        if (_ammount > 1)
        {
            newEffect += $"({_item.Effect * _ammount})";
        }
        StringHelper.Replace(ref ability, "<X>", newEffect);
        ItemTooltip.instance.Show(_item.DisplayName, ability);
    }

    public void HideTooltip()
    {
        ItemTooltip.instance.Hide();
    }
}
