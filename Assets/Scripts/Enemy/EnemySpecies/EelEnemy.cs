using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelEnemy : Enemy
{
    [SerializeField] private float _period;

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(Random.Range(0, _period));
        while (true)
        {
            ProjectileWaveMovement newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileWaveMovement>();
            newProjectile.Init(Direction);
            yield return new WaitForSeconds(_period);
        }
        yield break;
    }
}
