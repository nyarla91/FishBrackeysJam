using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindow : MonoBehaviour
{
    protected virtual void Awake()
    {
        Shop.AddIShopWindow(GetComponent<IShopWindow>());
    }
}
