using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private AssetItem item;
    [SerializeField] private float pickUpLockTime;
    private bool canPickUp = true;

    protected override void Start()
    {
        base.Start();
        if (item.UIIcon != null)
            GetComponentInParent<SpriteRenderer>().sprite = item.UIIcon;
        if (item.Name != null)
            transform.parent.name = item.Name;
    }

    public void Init(AssetItem newItem)
    {
        item = newItem;
        if (item.UIIcon != null)
            GetComponentInParent<SpriteRenderer>().sprite = item.UIIcon;

        StartCoroutine(LockPickUp(pickUpLockTime));
    }
    public override void Interact(Transform target)
    {
        Inventory inv = target.GetComponentInChildren<Inventory>();
        if (canPickUp && inv != null && inv.AddItem(item))
            Destroy(transform.parent.gameObject);
    }
    

    private IEnumerator LockPickUp(float time)
    {
        canPickUp = false;
        yield return new WaitForSeconds(time);
        canPickUp = true;
    }
}
