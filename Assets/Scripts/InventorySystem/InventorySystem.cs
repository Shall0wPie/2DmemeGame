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

    [SerializeField]
    private InventorySlot inventorySlotTemplate;
    private Transform player;
    [SerializeField] private Transform _container;

    [SerializeField] private Transform _draggingParent;
    //[SerializeField] private ItemsEjector _ejector;
    public void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        // Debug.Log(slots.Count);
        // foreach (Transform child in _container)
        //     Destroy(child.gameObject);
        Debug.Log(slots.Count);
        foreach (InventorySlot slot in slots)
        {
            //InventorySlot cell = Instantiate(inventorySlotTemplate, _container);
            //cell.Init(_draggingParent);
            //slot.Render();          
            slot.Ejecting += () =>
            {
                slots.Remove(slot);
                Destroy(slot.gameObject);
            };
            //slot.Ejecting += () => DropItem(slot);
        }
    }

    public bool AddItem(AssetItem item)
    {
        if (item.stackSize > 1)
            foreach (InventorySlot slot in slots)
            {
                if (slot.Contains(item) && slot.count < item.stackSize)
                {
                    slot.PushItem(item);
                    return true;
                }
            }

        InventorySlot newSlot = Instantiate(inventorySlotTemplate, _container);
        newSlot.Init(_draggingParent, item);
        newSlot.PushItem(item);
        slots.Add(newSlot);
        Refresh();

        return true;
    }
    public void DropItem(InventorySlot slot)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (slot != null)
        {
            Vector2 position = new Vector2(player.position.x + 5f * player.lossyScale.x, player.position.y);
            Debug.Log(position);
            InventorySlot newItem  = Instantiate(slot, position, Quaternion.identity);
            //Item.SpawnItem(slots[selectedSlot].PopItem(), position);
        }
    }
}
