using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;
using UnityEngine.UI;

public class ShopTab : Transformer
{
    [SerializeField] private RectTransform _window;
    [SerializeField] private Image _image;
    [SerializeField] private List<ShopTab> _otherTabs;

    public bool Opened { get; set; }

    private void FixedUpdate()
    {
        float newY = Mathf.Lerp(rectTransform.anchoredPosition.y, Opened ? 25 : 0, 0.3f);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
        float newAlpha = Mathf.Lerp(_image.color.a, Opened ? 1 : 0.8f, 0.3f);
        _image.color = VectorHelper.SetA(_image.color, newAlpha);
    }

    public void Open()
    {
        _window.SetAsLastSibling();
        Opened = true;
        foreach (var otherTab in _otherTabs)
        {
            otherTab.Opened = false;
        }
    }
}
