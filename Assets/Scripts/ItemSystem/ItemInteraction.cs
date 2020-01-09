using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Intreactable
{
    public Item item;
    private void Start()
    {
        Init();
    }
    // Start is called before the first frame update
    public override void Interact()
    {
        if (Inventory.instance.AddItem(item))
            Destroy(gameObject);
    }

    protected override void Init()
    {
        base.Init();
        item = GetComponent<Item>();
        if (item.icon != null)
            GetComponent<SpriteRenderer>().sprite = item.icon;
    }
}
