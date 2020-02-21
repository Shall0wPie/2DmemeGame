using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerUI : MonoBehaviour
{
    [SerializeField] private Container itemContainer;
    public Transform content;
    public Transform inventoryWindow;
    
    [Space][Header("Templates")]
    [SerializeField] private InventorySlot inventorySlotTemplate;
    [SerializeField] private GameObject SceneItemTemplate;

    private InventorySlot[] inventorySlots;

    private void Start()
    {
        itemContainer.OnChange += Refresh;
    }

    public void Refresh()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);
        
        foreach (ItemSlot slot in itemContainer.itemSlots)
        {
            InventorySlot cell = Instantiate(inventorySlotTemplate, content);
            cell.Init(transform.parent, slot);
        }
    }

    public void ReArrange()
    {
        itemContainer.itemSlots.Clear();
        inventorySlots = content.GetComponentsInChildren<InventorySlot>();
        Debug.Log(inventorySlots.Length);
        foreach (InventorySlot slot in inventorySlots)
        {
            itemContainer.itemSlots.Add(slot.ItemSlot);
        }
    }
}