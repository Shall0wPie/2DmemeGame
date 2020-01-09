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
    private Transform player;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public delegate void OnSelectorChanged();
    public OnSelectorChanged OnSelectorChangedCallBack;

    void Start()
    {
        items = new List<Item>(inventorySize);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool AddItem(Item item)
    {
        Item existing = items.Find(x => x.ItemName == item.ItemName);
        bool founded = items.Exists(x => x.ItemName == item.ItemName);

        if (founded && existing.stackable && existing.quantity < existing.maxStack)
        {
            existing.quantity++;
            return true;
        }

        if (items.Count < inventorySize)
        {
            items.Add(item);
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
            return true;
        }
        return false;
    }

    public void DropItem()
    {
        if (items.Count > selectedSlot)
        {
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
        if (items.Count > selectedSlot)
        {
            items[selectedSlot].Use(player);

            if (!items[selectedSlot].stackable || items[selectedSlot].quantity <= 0)
                items.RemoveAt(selectedSlot);

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
    }
}
