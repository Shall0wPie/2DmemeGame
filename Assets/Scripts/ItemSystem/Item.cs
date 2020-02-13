using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ItemName;
    public Sprite icon;
    public int stackSize;

    public static Transform SpawnItem(Item item, Vector2 position)
    {
        Transform newTransform = Instantiate(Prefabs.instance.item, position, Quaternion.identity);
        newTransform.GetComponent<ItemInteractable>().item = item;

        return newTransform;
    }

    public abstract void Use(Transform target);
}
