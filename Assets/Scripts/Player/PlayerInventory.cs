using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private FishInfo _victoryLoot;
    public List<FishInfo> fish = new List<FishInfo>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<LootOnGround>() != null)
        {
            LootOnGround otherLoot = other.gameObject.GetComponent<LootOnGround>();
            if (otherLoot.Loot.Equals(_victoryLoot))
            {
                Result.Show(true);
            }
            fish.Add(otherLoot.Loot);
            Destroy(other.gameObject);
        }
    }
}
