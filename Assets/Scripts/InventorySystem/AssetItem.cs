using System;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemForInventory")]
public class AssetItem : ScriptableObject, IItem
{
    public string Name => _name;
    public Sprite UIIcon => _uiIcon;
    public int stackSize => _stackSize;

    [SerializeField] private int _stackSize;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _uiIcon;
}
