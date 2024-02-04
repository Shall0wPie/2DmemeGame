using System;
using System.Collections.Generic;
using UnityEngine;


// Contains all slots of current container
public class Container : MonoBehaviour
{
    public List<ItemSlot> itemSlots; // Represents actual slots
    public event Action OnChange; // event on change quantity of items in one slot
    public event Action OnAdd; // event on creation of new slot

    [Tooltip("Declares the size of the container (0 = infinite size)")] 
    [SerializeField] private int _containerSize;

    public int containerSize
    {
        get => _containerSize;
        private set => _containerSize = value;
    }

    private void Start()
    {
        itemSlots = new List<ItemSlot>();
    }

    // Adds item to the existing slot or creates new if there's no free slots
    public bool AddToContainer(Item item)
    {
        if (item.stackSize > 1) // Stacks items only if they can be stacked
            foreach (ItemSlot slot in itemSlots)
            {
                if (slot.item.Equals(item) && slot != InventoryUI.draggableSlot && slot.count < item.stackSize)
                {
                    slot.PutItem();
                    OnChange?.Invoke();
                    return true;
                }
            }

        // Creates new slot gor item
        if (_containerSize == 0 || itemSlots.Count < _containerSize)
        {
            itemSlots.Add(new ItemSlot(item));
            OnAdd?.Invoke();
            return true;
        }

        return false;
    }

    // Removes slot from list
    public void RemoveFromContainer(ItemSlot slot)
    {
        itemSlots.Remove(slot);
    }
}