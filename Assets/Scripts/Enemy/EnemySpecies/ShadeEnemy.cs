using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NyarlaEssentials;
using UnityEngine;

public class ShadeEnemy : Enemy
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _cooldownBetweenAttacks;
    [SerializeField] private float _attacksDelay;
    [SerializeField] private List<string> _states = new List<string>();

    [Header("Basic attaclk")]
    [SerializeField] private float _period;
    [SerializeField] private float _speed;

    [Header("Spikes attack")]
    [SerializeField] private GameObject _spikePrefab;
    [SerializeField] private int _spikesNumber;
    
    [Header("Rain attack")]
    [SerializeField] private GameObject _rainPrefab;
    [SerializeField] private int _rainNumber;
    [SerializeField] private float _rainSpeed;

    [Header("BeamAttack")]
    [SerializeField] private GameObject _beamPrefab;
    [SerializeField] private int _beamRepeats;
    [SerializeField] private float _beamDuration;

    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(7 ,0, transform.position.z);
        StartCoroutine(BasicAttack());
        StartCoroutine(AttacksCycle());
    }

    private IEnumerator BasicAttack()
    {
        while (true)
        {
            ProjectileDirectionMovement newProjectile =
                ProjectileLifecycle.Create<ProjectileDirectionMovement>(_projectilePrefab, transform.position);
            newProjectile.Init(Direction, _speed);
            yield return new WaitForSeconds(_period);
        }
    }

    private IEnumerator AttacksCycle()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            string newAttack = CollectionHelper.ChooseRandomElement(_states);
            _animator.Play(newAttack);
            yield return new WaitForSeconds(_attacksDelay);
            switch (newAttack)
            {
                case "spikes":
                {
                    yield return StartCoroutine(SpikesAttack());
                    break;
                }
                case "rain":
                {
                    yield return StartCoroutine(RainAttack());
                    break;
                }
                case "beam":
                {
                    yield return StartCoroutine(BeamAttack());
                    break;
                }
            }

            _animator.Play("start");
            yield return new WaitForSeconds(_cooldownBetweenAttacks);
        }
    }

    private IEnumerator SpikesAttack()
    {
        for (int i = 0; i < _spikesNumber; i++)
        {
            Vector3 position = Player.Transform.position;
            if (i > 0)
                position = VectorHelper.RandomPointInBounds(CombatArea.Bounds);
            Transform newProjectile = Instantiate(_spikePrefab, position, Quaternion.identity).transform;
            newProjectile.parent = Map.transform;
        }
        yield return new WaitForSeconds(4);
        yield break;
    }

    private IEnumerator RainAttack()
    {
        for (int i = 0; i < _rainNumber; i++)
        {
            Vector3 position = VectorHelper.RandomPointInBounds(CombatArea.Bounds) + new Vector2(17, 0);
            ProjectileDirectionMovement newProjectile = Instantiate(_rainPrefab, position, Quaternion.identity)
                .GetComponent<ProjectileDirectionMovement>();
            newProjectile.transform.parent = transform;
            newProjectile.Init(Vector2.left, _rainSpeed);
        }
        yield return new WaitForSeconds(9);
        yield break;
    }

    private IEnumerator BeamAttack()
    {
        float lastDegree = 180;
        for (int i = 0; i < _beamRepeats; i++)
        {
            Transform beam = Instantiate(_beamPrefab, transform.position, Quaternion.Euler(0, 0, lastDegree)).transform;
            beam.parent = transform;
            float toPlayerDegree = VectorHelper.Vector2ToDegrees(Player.Transform.position - transform.position);
            if (toPlayerDegree < 0)
                toPlayerDegree += 360;
            for (float j = 0; j < _beamDuration; j += Time.deltaTime)
            {
                if (beam != null)
                {
                    lastDegree = Mathf.Lerp(beam.rotation.eulerAngles.z, toPlayerDegree, Time.deltaTime);
                    beam.rotation = Quaternion.Euler(0, 0, lastDegree);
                    yield return null;
                }
            }
            if (beam != null)
                Destroy(beam.gameObject);
        }
        yield return new WaitForSeconds(2);
    }
}
