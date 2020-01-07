using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        HealthPotion1,
        HealthPotion2,
        HealthPotion3
    }

    [SerializeField] private IEffect effect;

    public ItemType itemType;
    public Sprite icon;


    public static void SpawnItem(Item item, Vector2 position)
    {
        Transform newTransform = Instantiate(Prefabs.instance.itemPrefab, position, Quaternion.identity);
        newTransform.GetComponent<Item>().SetItem(item);
    }

    void Start()
    {
        if (icon != null)
            GetComponent<SpriteRenderer>().sprite = icon;
    }

    public void SetItem(Item item)
    {
        itemType = item.itemType;
        icon = item.icon;
    }
}
