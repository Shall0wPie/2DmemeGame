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
    
    public static Transform SpawnItem(Item item, Vector2 position, Vector2 velocity)
    {
        Transform newItem = Instantiate(Prefabs.instance.item, position, Quaternion.identity);
        newItem.GetComponentInChildren<ItemPickup>().Init(item);
        newItem.GetComponent<Rigidbody2D>().velocity = velocity;

        return newItem;
    }
}