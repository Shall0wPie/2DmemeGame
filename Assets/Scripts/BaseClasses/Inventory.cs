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
    private uint inventorySzie = 4;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    void Start()
    {
        itemList = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        if (itemList.Count < 4)
        {
            itemList.Add(item);
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
            return true;
        }
        return false;
    }

    public void DropItem(int itemIndex)
    {
        if (itemIndex < itemList.Count)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 position = new Vector2(player.position.x + 5f * player.lossyScale.x, player.position.y);
            
            Item.SpawnItem(itemList[itemIndex], position);
            itemList.RemoveAt(itemIndex);
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
    }
}
