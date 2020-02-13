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

    [SerializeField] private List<InventorySlot> slots;

    [SerializeField]
    private InventorySlot inventorySlotTemplate;

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
            Debug.Log("ref");
            slot.Ejecting += () =>
            {
                slots.Remove(slot);
                Destroy(slot.gameObject);
            };
            //slot.Ejecting += () => _ejector.EjectFromPool(item, _ejector.transform.position, _ejector.transform.right);
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
}