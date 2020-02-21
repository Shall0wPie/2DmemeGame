using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Container> containers;

    [Space] [Header("Templates")] [SerializeField]
    private InventorySlot inventorySlotTemplate;

    [SerializeField] private GameObject SceneItemTemplate;

    public bool AddItem(AssetItem item)
    {
        foreach (Container container in containers)
        {
            if (container.AddToContainer(item))
            {
                return true;
            }
        }

        return false;
    }

    public void DropItem(AssetItem item, int dropAmount, Vector2 velocity)
    {
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject newItem = Instantiate(SceneItemTemplate, transform.position, Quaternion.identity);
            newItem.GetComponentInChildren<ItemPickup>().Init(item);
            newItem.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}