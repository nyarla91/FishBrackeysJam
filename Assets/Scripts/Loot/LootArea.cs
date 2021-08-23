using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class LootArea : MonoBehaviour
{
    private static LootArea _instance;

    [SerializeField] private BoxCollider2D _collider;

    private void Awake()
    {
        _instance = this;
    }

    public static Vector2 RandomPoint() => VectorHelper.RandomPointInBounds(_instance._collider.bounds);
}
