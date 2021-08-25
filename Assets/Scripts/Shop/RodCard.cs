using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RodCard : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    [SerializeField] private RodCard _currentRod;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _ability;
    [SerializeField] private TextMeshProUGUI _hookDamage;
    [SerializeField] private TextMeshProUGUI _meleeDamage;
    [SerializeField] private GameObject _soldOutOverlay;

    private RodInfo _rod;
    private bool _sold;

    private bool Sold
    {
        get => _sold;
        set
        {
            _sold = value;
            if (!Current)
                _soldOutOverlay.SetActive(value);
        }
    }

    private bool Current => _soldOutOverlay == null;

    private int _countedCost;

    public void Init(RodInfo rod)
    {
        Sold = false;
        _rod = rod;
        _name.text = _rod.Name;
        _countedCost = Mathf.RoundToInt(rod.Cost * Rounds.CostMultiplier);
        _cost.text = $"{_countedCost} $";
        _ability.text = _rod.Description;
        _sprite.sprite = _rod.Sprite;
        _hookDamage.text = Mathf.RoundToInt(_rod.HookDamage * Rounds.RodStatsMultiplier).ToString();
        _meleeDamage.text = Mathf.RoundToInt(_rod.MeleeDamage * Rounds.RodStatsMultiplier).ToString();
    }

    public void Buy()
    {
        if (!Current && Shop.Money >= _countedCost)
        {
            Shop.Money -= _countedCost;
            Sold = true;
            Rods.CurrentRod = _rod;
            Rods.CurrentRodStatsMultiplier = Rounds.RodStatsMultiplier;
            _currentRod.Init(Rods.CurrentRod);
        }
    }
}
