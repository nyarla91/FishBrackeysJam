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

    public static T Create<T>(GameObject prefab, Vector3 position)
    {
        T newProjectile = Instantiate(prefab, position, Quaternion.identity).GetComponent<T>();
        return newProjectile;
    }
}
