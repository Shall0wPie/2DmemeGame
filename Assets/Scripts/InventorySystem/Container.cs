using System;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;


public class Container : MonoBehaviour
{
    public List<ItemSlot> itemSlots;
    public event Action OnChange;
    public event Action OnAdd;

    public bool AddToContainer(AssetItem item)
    {
        if (item.stackSize > 1)
            foreach (ItemSlot slot in itemSlots)
            {
                if (slot.item.Equals(item) && slot != InventoryUI.dragingSlot && slot.count < item.stackSize)
                {
                    slot.PutItem();
                    OnChange?.Invoke();
                    return true;
                }
            }

        itemSlots.Add(new ItemSlot(item));
        OnAdd?.Invoke();
        return true;
    }

    public void RemoveFromContainer(ItemSlot slot)
    {
        Debug.Log(itemSlots.Count);
        itemSlots.Remove(slot);
        Debug.Log(itemSlots.Count);
    }
}