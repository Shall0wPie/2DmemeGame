using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string ItemName;
    public Sprite icon;
    public int quantity;
    public bool stackable;
    public int maxStack;

    public static void SpawnItem(Item item, Vector2 position)
    {
        Transform newTransform = Instantiate(Prefabs.instance.item, position, Quaternion.identity);
        newTransform.GetComponent<ItemInteraction>().item.SetItem(item);
    }

    public abstract void Use(Transform target);

    public void SetItem(Item item)
    {
        ItemName = item.ItemName;
        icon = item.icon;
        quantity = item.quantity;
        stackable = item.stackable;
        maxStack = item.maxStack;
    }
}
