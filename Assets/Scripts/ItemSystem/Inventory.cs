using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance { get; private set; }
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

    public int selectedSlot { get; private set; }
    public int slotsCount { get; private set; }

    public SlotUI[] slots { get; private set; }
    private Transform player;

    void Awake()
    {
        slots = GetComponentsInChildren<SlotUI>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        slotsCount = slots.Length;
        SelectSlot(0);
    }

    private void OnLevelWasLoaded(int level)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool AddItem(Item item)
    {
        if (item.stackSize > 1)
            foreach (SlotUI slot in slots)
            {
                if (slot.Contains(item) && slot.count < item.stackSize)
                {
                    slot.PushItem(item);
                    return true;
                }
            }

        foreach (SlotUI slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.PushItem(item);
                return true;
            }
        }

        return false;
    }

    public void DropItem()
    {
        if (!slots[selectedSlot].IsEmpty())
        {
            Vector2 position = new Vector2(player.position.x + 5f * player.lossyScale.x, player.position.y);

            Item.SpawnItem(slots[selectedSlot].PopItem(), position);
        }
    }

    public void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex;
        if (selectedSlot >= slots.Length)
            selectedSlot = slots.Length - 1;
        else if (selectedSlot < 0)
            selectedSlot = 0;

        foreach (SlotUI slot in slots)
        {
            slot.GetComponent<Image>().color = Color.white;
        }
        slots[selectedSlot].GetComponent<Image>().color = Color.green;
    }

    public void UseItem()
    {
        if (!slots[selectedSlot].IsEmpty())
        {
            slots[selectedSlot].PopItem().Use(player);
        }
    }
}
