using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private RectTransform _healthBackground;
    [SerializeField] private RectTransform _healthForeground;

    public void UpdateHealth(float percent)
    {
        print(percent);
        _healthForeground.sizeDelta = new Vector2(_healthBackground.rect.width * percent, 0);
    }
}
