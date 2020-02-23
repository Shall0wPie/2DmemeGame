using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// Visualise container slots and contains them 
public class ContainerUI : MonoBehaviour
{
    public Container itemContainer; // Contaner to which it is attached
    public Transform content; // UI parent of InventorySlots
    public Transform inventoryWindow; // Whole inventory panel

    [Space] [Header("Templates")] [SerializeField]
    private InventorySlot inventorySlotTemplate;

    private InventorySlot[] inventorySlots;

    private void Start()
    {
        itemContainer.OnChange += Refresh;
        itemContainer.OnAdd += AddNewSlot;
    }

    // If item was added to Container and there was free slot then recalculate slots' counters
    private void Refresh()
    {
        inventorySlots = content.GetComponentsInChildren<InventorySlot>();

        foreach (InventorySlot slot in inventorySlots)
        {
            slot.UpdateCounter();
        }
    }

    // If their wasn't free slot then creates new
    private void AddNewSlot()
    {
        InventorySlot cell = Instantiate(inventorySlotTemplate, content);
        cell.Init(this, itemContainer.itemSlots.Last());
    }

    public void DropFromSlot(int index, int quantity)
    {
        inventorySlots = content.GetComponentsInChildren<InventorySlot>();
        if (index >= inventorySlots.Length)
            return;

        inventorySlots[index].DropItems(quantity);
    }

    public void UseFromSlot(int index)
    {
        inventorySlots = content.GetComponentsInChildren<InventorySlot>();
        if (index >= inventorySlots.Length)
            return;

        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        inventorySlots[index].UseItem(target);
    }
}