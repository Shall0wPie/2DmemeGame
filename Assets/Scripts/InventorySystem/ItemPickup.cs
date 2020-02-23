using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private Item item;
    [SerializeField] private float pickUpLockTime;
    private bool canPickUp = true;

    private void Start()
    {
        if (item.uiIcon != null)
            GetComponentInParent<SpriteRenderer>().sprite = item.uiIcon;
        if (item.name != null)
            transform.parent.name = item.name;
    }

    public void Init(Item newItem)
    {
        item = newItem;
        if (item.uiIcon != null)
            GetComponentInParent<SpriteRenderer>().sprite = item.uiIcon;

        StartCoroutine(LockPickUp(pickUpLockTime));
    }
    
    // Calls this method when interaction happens
    public override void Interact(Transform target)
    {
        Inventory inv = target.GetComponentInChildren<Inventory>();
        if (canPickUp && inv != null && inv.AddItem(item))
            Destroy(transform.parent.gameObject);
    }

    // Disables interaction for given amount of time
    private IEnumerator LockPickUp(float time)
    {
        canPickUp = false;
        yield return new WaitForSeconds(time);
        canPickUp = true;
    }
}
