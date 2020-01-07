using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Intreactable
{
    public Item item;
    // Start is called before the first frame update
    public override void Interact()
    {
        if (Inventory.instance.AddItem(item))
            Destroy(gameObject);
    }
}
