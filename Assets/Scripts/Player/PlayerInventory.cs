using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<EnemyLoot> _fish = new List<EnemyLoot>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<LootOnGround>() != null)
        {
            _fish.Add(other.gameObject.GetComponent<LootOnGround>().Loot);
            Destroy(other.gameObject);
        }
    }
}
