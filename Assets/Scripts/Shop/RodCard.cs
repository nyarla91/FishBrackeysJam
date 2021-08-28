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
    public RodInfo Rod => _rod;
    private bool _sold;

    public bool Sold
    {
        get => _sold;
        private set
        {
            _sold = value;
            if (!Current)
                _soldOutOverlay.SetActive(value);
        }
    }

    public bool Current => _soldOutOverlay == null;

    private int _countedCost;

    public void Init(RodInfo rod)
    {
        Sold = false;
        _rod = rod;
        _name.text = _rod.DisplayName;
        _countedCost = 0;
        if (!Rods.CurrentRod.Name.Equals("golden"))
            _countedCost = Mathf.RoundToInt(rod.Cost * Rounds.CostMultiplier);
        if (Current)
            _cost.text = $"";
        else
            _cost.text = $"{_countedCost} $";
        _ability.text = _rod.Description;
        _sprite.sprite = _rod.SpriteFull;
        if (Current)
        {
            _hookDamage.text = Mathf.RoundToInt(_rod.HookDamage * Rods.CurrentRodStatsMultiplier).ToString();
            _meleeDamage.text = Mathf.RoundToInt(_rod.MeleeDamage * Rods.CurrentRodStatsMultiplier).ToString();
        }
        else
        {
            _hookDamage.text = Mathf.RoundToInt(_rod.HookDamage * Rounds.RodStatsMultiplier).ToString();
            _meleeDamage.text = Mathf.RoundToInt(_rod.MeleeDamage * Rounds.RodStatsMultiplier).ToString();
        }
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
