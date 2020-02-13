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
    
    [SerializeField] private List<InventorySlot> Items;
    [FormerlySerializedAs("_inventoryCellTemplate")] [SerializeField] private InventorySlot inventorySlotTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;
    //[SerializeField] private ItemsEjector _ejector;
    public void OnEnable()
    {
        Refresh(Items);
    }

    public void Refresh(List<InventorySlot> items)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);

        foreach (InventorySlot item in items)
        {
            InventorySlot slot = Instantiate(inventorySlotTemplate, _container);
            slot.Init(_draggingParent);
            slot.Render(item);
            slot.Ejecting += () => Destroy(slot.gameObject);
            //slot.Ejecting += () => _ejector.EjectFromPool(item, _ejector.transform.position, _ejector.transform.right);
        }
    }
    
    public bool AddItem(IItem item)
    {
        if (item.stackSize > 1)
            foreach (InventorySlot slot in Items)
            {
                if (slot.Contains(item) && slot.count < item.stackSize)
                {
                    slot.PushItem(item);
                    return true;
                }
            }
    
        foreach (InventorySlot slot in Items)
        {
            if (slot.IsEmpty())
            {
                slot.PushItem(item);
                return true;
            }
        }
    
        return false;
    }
} 
