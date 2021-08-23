using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<FishInfo> fish = new List<FishInfo>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<LootOnGround>() != null)
        {
            fish.Add(other.gameObject.GetComponent<LootOnGround>().Loot);
            Destroy(other.gameObject);
        }
    }
}
