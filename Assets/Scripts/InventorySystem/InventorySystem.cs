using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventorySystem : MonoBehaviour
{
    #region Singleton

    public static InventorySystem instance { get; private set; }

    private void Start()
    {
        if (instance != null)
        {
            //Debug.Log("Inventory is alredy exists");
            Destroy(gameObject);
        }
        else
            instance = this;
    }

    #endregion

    [SerializeField] public List<InventorySlot> slots;

    [SerializeField] private InventorySlot inventorySlotTemplate;
    [SerializeField] private ItemPickup SceneItemTemplate;

    private Transform player;
    [SerializeField] private Transform _container;

    [SerializeField] private Transform _draggingParent;

    public bool AddItem(AssetItem item)
    {
        if (item.stackSize > 1)
            foreach (InventorySlot slot in slots)
            {
                if (slot.Contains(item) && slot.counter < item.stackSize)
                {

                    slot.PushItem(item);
                    return true;
                }
            }

        InventorySlot newSlot = Instantiate(inventorySlotTemplate, _container);
        newSlot.Init(_draggingParent, item);
        newSlot.PushItem(item);
        slots.Add(newSlot);
        return true;
    }
    public void DropItem(InventorySlot slot, int dropAmount)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        ItemPickup newItem;
        if (slot != null)
        {
            Debug.Log(slot.GetItem().Count);
            Vector2 position = new Vector2(player.position.x + Random.Range(3f, 7f) * player.lossyScale.x, player.position.y);
            Stack<AssetItem> item = slot.GetItem();
            for (int i = dropAmount; i > 0 && item.Count > 0; i--)
            {

                newItem = Instantiate(SceneItemTemplate, position, Quaternion.identity);
                newItem.Init(item.Pop());

            }
            //newItem = Instantiate(item, position, Quaternion.identity);
            //Item.SpawnItem(slots[selectedSlot].PopItem(), position);
        }
    }
}
