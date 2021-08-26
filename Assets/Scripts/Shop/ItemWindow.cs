using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class ItemWindow : ShopWindow, IShopWindow
{
    [SerializeField] private ItemCard _permanentCard;
    [SerializeField] private List<ItemCard> _randomizedCards;
    
    [SerializeField] private ItemInfo _permanentItem;
    [SerializeField] private List<ItemPool> _itemPoolsPool;
    
    private List<ItemInfo> _itemsPool = new List<ItemInfo>();

    protected override void Awake()
    {
        base.Awake();
        foreach (var pool in _itemPoolsPool)
        {
            for (int i = 0; i < pool.Ammount; i++)
            {
                _itemsPool.Add(pool.Item);
            }
        }
    }

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

[System.Serializable]
public class ItemPool
{
    [SerializeField] private ItemInfo _item;
    public ItemInfo Item => _item;

    [SerializeField] private int ammount;
    public int Ammount => ammount;
}
