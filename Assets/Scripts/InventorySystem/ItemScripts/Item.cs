using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 1)]
public class Item : ScriptableObject, IItem
{
    public string name => _name;
    public Sprite uiIcon => _uiIcon;
    public int stackSize => _stackSize;

    [SerializeField] protected int _stackSize;
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _uiIcon;
}