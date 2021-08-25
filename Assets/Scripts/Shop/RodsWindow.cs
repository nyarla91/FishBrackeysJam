using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class RodsWindow : ShopWindow, IShopWindow
{
    [SerializeField] private RodCard _currentRod;
    [SerializeField] private List<RodCard> _randomizedRods;

    [SerializeField] private List<RodInfo> _rodPool;
    
    public void GenerateContent()
    {
        _currentRod.Init(Rods.CurrentRod);
        List<RodInfo> rods = CollectionHelper.TakeAwayRandomElements(ref _rodPool, _randomizedRods.Count);
        for (int i = 0; i < _randomizedRods.Count; i++)
        {
            _randomizedRods[i].Init(rods[i]);
        }
    }
}
