using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

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
        if (slot != null)
        {
            Vector2 position = new Vector2(player.position.x, player.position.y);
            Vector2 velocity = new Vector2(Random.Range(20f, 25f) * player.lossyScale.x, 0);
            
            Stack<AssetItem> item = slot.GetItem();
            for (int i = dropAmount; i > 0 && item.Count > 0; i--)
            {
                ItemPickup newItem = Instantiate(SceneItemTemplate, position, Quaternion.identity);
                newItem.Init(item.Pop());
                newItem.GetComponent<Rigidbody2D>().velocity = velocity;
            }
        }
    }
}