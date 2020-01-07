using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        HealthPotion1,
        HealthPotion2,
        HealthPotion3
    }

    public ItemType itemType;
    public Sprite icon;

    void Start()
    {
        icon = GetComponent<SpriteRenderer>().sprite;
    }
}
