using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _ui;
    [SerializeField] private RectTransform _healthBackground;
    [SerializeField] private RectTransform _healthForeground;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _itemPrefab;
    
    private Dictionary<ItemInfo, ItemInInventory> _items = new Dictionary<ItemInfo, ItemInInventory>();

    private Coroutine _hideShow;

    public void UpdateHealth(float health, float maxHealth)
    {
        float percent = health / maxHealth;
        _healthForeground.sizeDelta = new Vector2(_healthBackground.rect.width * percent, 0);
        _healthText.text = $"{Mathf.CeilToInt(health)} / {Mathf.CeilToInt(maxHealth)}";
    }

    public void AddItem(ItemInfo item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item].AddOne();
        }
        else
        {
            ItemInInventory newItem = Instantiate(_itemPrefab, _content).GetComponent<ItemInInventory>();
            newItem.rectTransform.anchoredPosition = new Vector2(25 + 100 * _items.Count, 0);
            _content.sizeDelta = new Vector2(25 + 100 * (_items.Count + 1), 0);
            newItem.Init(item);
            _items.Add(item, newItem);
        }
    }

    public void Hide()
    {
        _ui.interactable = _ui.blocksRaycasts = false;
        if (_hideShow != null)
            StopCoroutine(_hideShow);
        _hideShow = StartCoroutine(Blend(0));
    }

    public void Show()
    {
        _ui.interactable = _ui.blocksRaycasts = true;
        if (_hideShow != null)
            StopCoroutine(_hideShow);
        _hideShow = StartCoroutine(Blend(1));
    }

    private IEnumerator Blend(float targetAlpha)
    {
        while (Mathf.Abs(_ui.alpha - targetAlpha) > 0.01f)
        {
            _ui.alpha = Mathf.Lerp(_ui.alpha, targetAlpha, Time.deltaTime * 7);
            yield return null;
        }
        _ui.alpha = targetAlpha;
    }
}
