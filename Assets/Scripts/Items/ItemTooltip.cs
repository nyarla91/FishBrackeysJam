using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using TMPro;
using UnityEngine;

public class ItemTooltip : Transformer
{
    public static ItemTooltip instance;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _ability;
    [SerializeField] private Vector2 _mouseOffset;
    
    private void Awake()
    {
        instance = this;
    }

    public void Show(string name, string ability)
    {
        _canvasGroup.alpha = 1;
        rectTransform.position = Input.mousePosition + (Vector3) _mouseOffset;
        _ability.text = ability;
        _name.text = name;
        StartCoroutine(AdaptBox());
    }

    private IEnumerator AdaptBox()
    {
        yield return null;
        float height = _ability.bounds.size.y + 100;
        print(height);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }
}
