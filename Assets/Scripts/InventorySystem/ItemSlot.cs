
using System;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    public ItemSlot(Item item)
    {
        this.item = item;
        count = 1;
    }

    public void PutItem()
    {
        count++;
    }

    public void TakeItem(int quantity)
    {
        count -= quantity;
    }
}