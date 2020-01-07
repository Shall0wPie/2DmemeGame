using UnityEngine.UI;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    public Image icon;

    Item item;

    // Start is called before the first frame update
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
