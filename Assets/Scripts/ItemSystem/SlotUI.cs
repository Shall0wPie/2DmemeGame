using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    public Image icon;
    private Stack<Item> items = new Stack<Item>();
    public int count { get; private set; }

    // Start is called before the first frame update
    public void PushItem(Item newItem)
    {
        count++;
        items.Push(newItem);
        icon.sprite = newItem.icon;
        icon.enabled = true;
    }

    public Item PopItem()
    {
        count--;
        if (count == 0)
        {
            icon.sprite = null;
            icon.enabled = false;
        }

        return items.Pop();
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }

    public bool Contains(Item item)
    {
        return items.Contains(item);
    }
}
