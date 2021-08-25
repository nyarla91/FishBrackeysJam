using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishWindow : ShopWindow, IShopWindow
{
    private const float FISH_CARD_WIDTH = 300;
    
    private static FishWindow _instance;
    
    [SerializeField] private GameObject _fishCardPrefab;
    [SerializeField] private RectTransform _content;
    
    private Dictionary<FishInfo, FishCard> _fishCards = new Dictionary<FishInfo, FishCard>();

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }

    public void GenerateContent()
    {
        foreach (var fishCard in _fishCards.Values)
        {
            Destroy(fishCard.gameObject);
        }
        
        _content.sizeDelta = new Vector2(40, _content.sizeDelta.y);
        List<FishInfo> inventory = Player.Inventory.fish;
        _fishCards = new Dictionary<FishInfo, FishCard>();
        
        foreach (var fish in inventory)
        {
            if (_fishCards.ContainsKey(fish))
            {
                _fishCards[fish].Ammount++;
                continue;
            }
            FishCard newCard = Instantiate(_fishCardPrefab, _content).GetComponent<FishCard>();
            _content.sizeDelta = new Vector2(_content.sizeDelta.x + (FISH_CARD_WIDTH + 20), _content.sizeDelta.y);
            newCard.rectTransform.anchoredPosition = new Vector2(40 + _fishCards.Count * (FISH_CARD_WIDTH + 20), newCard.rectTransform.anchoredPosition.y);
            newCard.rectTransform.sizeDelta = new Vector2(FISH_CARD_WIDTH, -80);
            newCard.Init(fish);
            _fishCards.Add(fish, newCard);
        }
    }

    public static void RemoveCard(FishInfo fish)
    {
        if (_instance._fishCards.ContainsKey(fish))
        {
            _instance._fishCards.Remove(fish);
        }

        List<FishCard> fishCards = _instance._fishCards.Values.ToList();
        for (int i = 0; i < fishCards.Count; i++)
        {
            fishCards[i].TargetX = 40 + (FISH_CARD_WIDTH + 20) * i;
        }
    }
}
