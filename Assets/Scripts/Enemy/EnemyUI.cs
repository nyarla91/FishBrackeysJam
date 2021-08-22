using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : EnemyComponent
{
    [SerializeField] private RectTransform _healthBackground;
    [SerializeField] private RectTransform _healthForeground;

    public void SetHealth(float percent)
    {
        _healthForeground.sizeDelta = new Vector2(percent, 0f);
    }
}
