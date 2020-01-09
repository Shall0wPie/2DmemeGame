using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text textCounter;
    private Stack<Item> items = new Stack<Item>();
    public int count { get; private set; }

    // Start is called before the first frame update
    public void PushItem(Item newItem)
    {
        count++;
        items.Push(newItem);
        icon.sprite = newItem.icon;
        icon.enabled = true;
        UpdateCounter();
    }

    public Item PopItem()
    {
        count--;
        if (count == 0)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
        UpdateCounter();
        return items.Pop();
    }

    void UpdateCounter()
    {
        if (count < 2)
        {
            textCounter.enabled = false;
        }
        else
        {
            textCounter.enabled = true;
            textCounter.text = count.ToString();
        }
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
