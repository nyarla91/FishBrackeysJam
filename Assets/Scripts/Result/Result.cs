using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NyarlaEssentials;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    public static Result instance;
    public static int moneyEarned;
    public static int roundsPassed;
    public static float damageDealt;
    public static string lastRod = "Fisihng rod";
    private static float _startingTime;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private TextMeshProUGUI _moneyCounter;
    [SerializeField] private TextMeshProUGUI _damageCounter;
    [SerializeField] private TextMeshProUGUI _timeCounter;
    [SerializeField] private TextMeshProUGUI _roundsCounter;
    [SerializeField] private TextMeshProUGUI _lastRodCounter;

    private void Awake()
    {
        instance = this;
        _startingTime = Time.time;
    }

    public static void Show(bool victory)
    {
        Player.Movement.FreezeControls = 100;
        instance.StartCoroutine(instance.ShowEnumerator(victory));
    }

    public IEnumerator ShowEnumerator(bool victory)
    {
        _canvasGroup.interactable = _canvasGroup.blocksRaycasts = true;

        _resultText.text = victory ? "Victory!" : "Death";
        _lastRodCounter.text = lastRod;
        _moneyCounter.text = moneyEarned.ToString();
        _damageCounter.text = Mathf.RoundToInt(damageDealt).ToString();
        _roundsCounter.text = roundsPassed.ToString();
        _timeCounter.text = StringHelper.SecondsToFormatTime(Mathf.RoundToInt(Time.time - _startingTime), false);
        for (float i = 0; i < 1; i += Time.deltaTime * 2)
        {
            _canvasGroup.alpha = i;
            yield return null;
        }
    }

    public static void Reset()
    {
        moneyEarned = 0;
        damageDealt = 0;
        if (roundsPassed > 0)
        {
            roundsPassed = 0;
        }

        lastRod = "Fishing rod";
    }
}
