using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class Map : Transformer
{
    public static Transform transform;

    private void Awake()
    {
        transform = gameObject.transform;
    }
}
