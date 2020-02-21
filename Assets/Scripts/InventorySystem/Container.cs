using System;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;


public class Container : MonoBehaviour
{
    public List<ItemSlot> itemSlots;
    public event Action OnChange;

    public bool AddToContainer(AssetItem item)
    {
        if (item.stackSize > 1)
            foreach (ItemSlot slot in itemSlots)
            {
                if (slot.item.Equals(item) && slot.count < item.stackSize)
                {
                    slot.PutItem();
                    OnChange();
                    return true;
                }
            }

        itemSlots.Add(new ItemSlot(item));
        OnChange();
        return true;
    }
}