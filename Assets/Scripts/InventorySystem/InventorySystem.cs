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

    [SerializeField] private List<InventorySlot> Slots;

    [FormerlySerializedAs("_inventoryCellTemplate")] [SerializeField]
    private InventorySlot inventorySlotTemplate;

    [SerializeField] private Transform _container;

    [SerializeField] private Transform _draggingParent;

    //[SerializeField] private ItemsEjector _ejector;
    public void OnEnable()
    {
        Refresh(Slots);
    }

    public void Refresh(List<InventorySlot> slots)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);

        foreach (InventorySlot slot in slots)
        {
            InventorySlot cell = Instantiate(inventorySlotTemplate, _container);
            cell.Init(_draggingParent);
            cell.Render();
            cell.Ejecting += () => Destroy(slot.gameObject);
            //slot.Ejecting += () => _ejector.EjectFromPool(item, _ejector.transform.position, _ejector.transform.right);
        }
    }

    public bool AddItem(AssetItem item)
    {
        if (item.stackSize > 1)
            foreach (InventorySlot slot in Slots)
            {
                if (slot.Contains(item) && slot.count < item.stackSize)
                {
                    slot.PushItem(item);
                    return true;
                }
            }

        InventorySlot newSlot = Instantiate(inventorySlotTemplate, _container);
        Debug.Log(newSlot.nameField);
        Slots.Add(newSlot);
        
        Debug.Log(Slots.Count-1);
        Slots[Slots.Count-1].PushItem(item);
        
        return true;
    }
}