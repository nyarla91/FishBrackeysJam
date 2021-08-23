using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rounds : MonoBehaviour
{
    public static Rounds instance;

    [SerializeField] private BoxCollider2D _enemyPlacementBounds;
    [SerializeField] private Round[] _rounds;

    private Phase _currentPhase;
    public Phase CurrentPhase
    {
        get => _currentPhase;
        set
        {
            _currentPhase = value;
            bool shop = CurrentPhase == Phase.Shop;
            CameraControl.TargetPoint = new Vector2(shop ? -5 : 0, 0);
        }
    }

    public List<GameObject> enemies;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(OngoingRound(_rounds[0]));
    }

    private IEnumerator OngoingRound(Round currentRound)
    {
        CurrentPhase = Phase.Combat;
        float timeRemaning = currentRound.Duration;
        while (timeRemaning >= 0)
        {
            EnemyForRound newEnemy = CollectionHelper.ChooseRandomElement(currentRound.Enemies);

            Vector3 position = VectorHelper.RandomPointInBounds(_enemyPlacementBounds.bounds);
            enemies.Add(Instantiate(newEnemy.Enemy, position, Quaternion.identity));
            
            float delay = newEnemy.Level / currentRound.LevelDivider;
            yield return new WaitForSeconds(delay);
            timeRemaning -= delay;
        }
        yield return new WaitUntil(() => enemies.Count == 0);
        CurrentPhase = Phase.Shop;
    }
}

public enum Phase
{
    Combat,
    Shop
}