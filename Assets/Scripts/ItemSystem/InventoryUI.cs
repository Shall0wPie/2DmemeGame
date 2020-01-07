using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    SlotUI[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateSlots;
        inventory.OnSelectorChangedCallBack += UpdateSelector;

        slots = GetComponentsInChildren<SlotUI>();
    }

    void UpdateSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.itemList.Count)
                slots[i].AddItem(inventory.itemList[i]);
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    private void UpdateSelector()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().color = Color.white;
        }
        slots[inventory.selectedSlot].GetComponent<Image>().color = Color.green;
    }
}
