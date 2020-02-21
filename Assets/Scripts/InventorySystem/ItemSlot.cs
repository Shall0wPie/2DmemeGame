
using System;

[Serializable]
public class ItemSlot
{
    public AssetItem item;
    public int count;

    public ItemSlot(AssetItem item)
    {
        this.item = item;
        count = 1;
    }

    public void PutItem()
    {
        count++;
    }

    public void TakeItem()
    {
        count--;
    }
}