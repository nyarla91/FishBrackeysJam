using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class ItemWindow : ShopWindow, IShopWindow
{
    [SerializeField] private ItemCard _permanentCard;
    [SerializeField] private List<ItemCard> _randomizedCards;
    
    [SerializeField] private ItemInfo _permanentItem;
    [SerializeField] private List<ItemInfo> _itemsPool;

    public void GenerateContent()
    {
        _permanentCard.Init(_permanentItem);
        List<ItemInfo> items = CollectionHelper.TakeAwayRandomElements(ref _itemsPool, _randomizedCards.Count);
        for (int i = 0; i < _randomizedCards.Count; i++)
        {
            _randomizedCards[i].Init(items[i]);
        }
    }
}
