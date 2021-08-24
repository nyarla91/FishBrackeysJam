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

    private Phase _currentPhase = Phase.Shop;
    public Phase CurrentPhase
    {
        get => _currentPhase;
        set
        {
            bool shop = value == Phase.Shop;
            CameraControl.TargetPoint = new Vector2(shop ? -5 : 0, 0);
            if (shop)
            {
                Shop.GenerateContent();
            }
            else if (_currentPhase == Phase.Shop)
            {
                if (currentRoundIndex < _rounds.Length)
                    StartCoroutine(OngoingRound(_rounds[currentRoundIndex]));
                currentRoundIndex++;
            }
            _currentPhase = value;
        }
    }

    public List<GameObject> enemies;
    private int currentRoundIndex;

    private void Awake()
    {
        instance = this;
        CurrentPhase = Phase.Combat;
    }

    private IEnumerator OngoingRound(Round currentRound)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < currentRound.Waves.Count; i++)
        {
            for (float j = 0; j < currentRound.Waves[i];)
            {
                EnemyForRound newEnemy = CollectionHelper.ChooseRandomElement(currentRound.Enemies);

                Vector3 position = VectorHelper.RandomPointInBounds(_enemyPlacementBounds.bounds);
                enemies.Add(Instantiate(newEnemy.Enemy, position, Quaternion.identity));

                j += newEnemy.Level;
            }
            yield return new WaitUntil(() => enemies.Count == 0);
            yield return new WaitForSeconds(1);
        }
        CurrentPhase = Phase.Shop;
    }
}

public enum Phase
{
    Combat,
    Shop
}