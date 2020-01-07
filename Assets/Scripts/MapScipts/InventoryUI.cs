using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    SlotUI[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = GetComponentsInChildren<SlotUI>();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.itemList.Count)
                slots[i].AddItem(inventory.itemList[i]);
            else
                slots[i].ClearSlot();
        }
    }
}
