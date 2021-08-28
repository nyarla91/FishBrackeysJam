using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatArea : MonoBehaviour
{
    private static CombatArea _instance;

    [SerializeField] private BoxCollider2D _collider;

    public static Bounds Bounds => _instance._collider.bounds;

    private void Awake()
    {
        _instance = this;
    }
}
