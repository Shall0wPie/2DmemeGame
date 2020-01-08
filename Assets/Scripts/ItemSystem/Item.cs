using UnityEngine;

public abstract class Item : ScriptableObject
{
    public Sprite icon;

    public static void SpawnItem(Item item, Vector2 position)
    {
        Transform newTransform = Instantiate(Prefabs.instance.item, position, Quaternion.identity);
        newTransform.GetComponent<ItemInteraction>().item.SetItem(item);
    }

    public abstract void Use(Transform target);

    public void SetItem(Item item)
    {
        icon = item.icon;
    }
}
