using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private int _cost;
    public int Cost => _cost;
    
    [SerializeField] private string _displayName;
    public string DisplayName => _displayName;
    
    [SerializeField] private string _name;
    public string Name => _name;
    
    [SerializeField] private int _effect;
    public int Effect => _effect;

    [SerializeField] [TextArea(2, 10)] private string _displayAbility;

    public string DisplayAbility => _displayAbility.Replace("<X>", _effect.ToString());
    public string PureDisplayAbility => _displayAbility;
}
