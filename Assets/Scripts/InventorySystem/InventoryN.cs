using System.Collections.Generic;
using UnityEngine;

public class InventoryN : MonoBehaviour
{
    [SerializeField] private List<Container> containers;
    
    [Space][Header("Templates")]
    [SerializeField] private InventorySlot inventorySlotTemplate;
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

    public void DropItem(InventorySlot slot, int dropAmount)
    {
    }
}