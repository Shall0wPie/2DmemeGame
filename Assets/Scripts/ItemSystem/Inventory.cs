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

    public List<Item> itemList;
    public int selectedSlot { get; private set; }
    private int inventorySzie = 4;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public delegate void OnSelectorChanged();
    public OnSelectorChanged OnSelectorChangedCallBack;

    void Start()
    {
        itemList = new List<Item>(4);
    }

    public bool AddItem(Item item)
    {
        if (itemList.Count < inventorySzie)
        {
            itemList.Add(item);
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
            return true;
        }
        return false;
    }

    public void DropItem()
    {
        if (itemList[selectedSlot] != null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 position = new Vector2(player.position.x + 5f * player.lossyScale.x, player.position.y);
            
            Item.SpawnItem(itemList[selectedSlot], position);
            itemList.RemoveAt(selectedSlot);
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
    }

    public void SelectItem(int slotIndex)
    {
        selectedSlot = slotIndex;
        if (selectedSlot >= inventorySzie)
        {
            selectedSlot = inventorySzie - 1;
        }

        if (OnSelectorChangedCallBack != null)
            OnSelectorChangedCallBack.Invoke();
    }
}
