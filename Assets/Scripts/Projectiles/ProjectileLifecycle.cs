using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using NyarlaEssentials;
using UnityEngine;

public class ProjectileLifecycle : Transformer
{
    [SerializeField] private bool _lerpScale = true;
    
    public delegate void DestroyHandler();
    public DestroyHandler OnProjectileDestroy;

    private void Start()
    {
        if (_lerpScale) StartCoroutine(ScaleUp());
    }

    private IEnumerator ScaleUp()
    {
        float targetScale = transform.localScale.x;
        transform.localScale = Vector3.zero;
        while (transform.localScale.x < targetScale * 0.99f)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, targetScale, Time.deltaTime * 6);
            yield return null;
        }
    }

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
