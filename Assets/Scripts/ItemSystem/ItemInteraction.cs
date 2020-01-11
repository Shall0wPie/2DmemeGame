using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Intreactable
{
    public Item item;
    protected override void Start()
    {
        base.Start();
        if (item.icon != null)
            GetComponent<SpriteRenderer>().sprite = item.icon;
    }
    public override void Interact()
    {
        if (Inventory.instance.AddItem(item))
            Destroy(gameObject);
    }
}
