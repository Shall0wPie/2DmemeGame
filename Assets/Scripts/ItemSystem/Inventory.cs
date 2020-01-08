using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Debug.Log("Inventory is aldeady exists");
        else
            instance = this;
    }
    #endregion

    public List<Item> items { get; private set; }
    public int selectedSlot { get; private set; }
    private int inventorySize = 4;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public delegate void OnSelectorChanged();
    public OnSelectorChanged OnSelectorChangedCallBack;

    void Start()
    {
        items = new List<Item>(inventorySize);
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (items.Count < inventorySize)
            {
                items.Add(item);
                if (onItemChangedCallBack != null)
                    onItemChangedCallBack.Invoke();
                return true;
            }
        }
        return false;
    }

    public void DropItem()
    {
        if (items.Count > selectedSlot)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 position = new Vector2(player.position.x + 5f * player.lossyScale.x, player.position.y);
            
            Item.SpawnItem(items[selectedSlot], position);
            items.RemoveAt(selectedSlot);
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
    }

    public void SelectItem(int slotIndex)
    {
        selectedSlot = slotIndex;
        if (selectedSlot >= inventorySize)
        {
            selectedSlot = inventorySize - 1;
        }

        if (OnSelectorChangedCallBack != null)
            OnSelectorChangedCallBack.Invoke();
    }

    public void UseItem()
    {
        items[selectedSlot].effect.Apply();
    }
}
