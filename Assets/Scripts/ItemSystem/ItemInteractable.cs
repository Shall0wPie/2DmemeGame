using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : Interactable
{
    public Item item;
    private void Start()
    {
        if (item.icon != null)
            GetComponent<SpriteRenderer>().sprite = item.icon;
    }
    public override void Interact(Transform target)
    {
        if (InventoryOld.instance.AddItem(item))
            Destroy(gameObject);
    }
}
