using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerUI : MonoBehaviour
{
    public Container itemContainer;
    public Transform content;
    public Transform inventoryWindow;
    
    [Space][Header("Templates")]
    [SerializeField] private InventorySlot inventorySlotTemplate;
    [SerializeField] private GameObject SceneItemTemplate;

    private InventorySlot[] inventorySlots;

    private void Start()
    {
        itemContainer.OnChange += Refresh;
        itemContainer.OnAdd += AddNewSlot;
    }

    public void Refresh()
    {
        inventorySlots = content.GetComponentsInChildren<InventorySlot>();
        // foreach (Transform child in content)
        //     Destroy(child.gameObject);
        
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.UpdateCounter();
        }
    }

    public void AddNewSlot()
    {
        InventorySlot cell = Instantiate(inventorySlotTemplate, content);
        cell.Init(this, itemContainer.itemSlots.Last());
    }

    public void ReArrange()
    {
        itemContainer.itemSlots.Clear();
        inventorySlots = content.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in inventorySlots)
        {
            itemContainer.itemSlots.Add(slot.itemSlot);
        }
    }
}