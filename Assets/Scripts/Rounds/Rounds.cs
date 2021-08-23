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

    private void Awake()
    {
        instance = this;
        StartCoroutine(OngoingRound(_rounds[0]));
    }

    private IEnumerator OngoingRound(Round currentRound)
    {
        float timeRemaning = currentRound.Duration;
        while (timeRemaning >= 0)
        {
            EnemyForRound newEnemy = CollectionHelper.ChooseRandomElement(currentRound.Enemies);
            
            float delay = newEnemy.Level / currentRound.LevelDivider;
            if (timeRemaning < currentRound.Duration) // First enemy appears without delay
                yield return new WaitForSeconds(delay);
            timeRemaning -= delay;
            
            Vector3 position = VectorHelper.RandomPointInBounds(_enemyPlacementBounds.bounds);
            Instantiate(newEnemy.Enemy, position, Quaternion.identity);
        }
    }
}
