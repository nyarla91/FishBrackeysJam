using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileLifecycle : MonoBehaviour
{
    public delegate void DestroyHandler();
    public DestroyHandler OnProjectileDestroy;
    
    private void OnBecameInvisible()
    {
        if (OnProjectileDestroy != null)
            OnProjectileDestroy();
        Destroy(gameObject);
    }
}
