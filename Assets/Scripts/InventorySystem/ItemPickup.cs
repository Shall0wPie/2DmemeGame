using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Intreactable
{
    public AssetItem item;
    protected override void Start()
    {
        base.Start();
        if (item.UIIcon != null)
            GetComponent<SpriteRenderer>().sprite = item.UIIcon;
    }
    public override void Interact()
    {
        if (InventorySystem.instance.AddItem(item))
            Destroy(gameObject);
    }
}
