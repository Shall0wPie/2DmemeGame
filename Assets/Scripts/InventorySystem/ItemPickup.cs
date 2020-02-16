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
            GetComponent<SpriteRenderer>().sprite = item.UIIcon;
    }

    public void Init(AssetItem newItem)
    {
        item = newItem;
        if (item.UIIcon != null)
            GetComponent<SpriteRenderer>().sprite = item.UIIcon;

        StartCoroutine(LockPickUp(pickUpLockTime));
    }
    public override void Interact()
    {
        if (canPickUp && InventorySystem.instance.AddItem(item))
            Destroy(gameObject);
    }
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (item.UIIcon != null && item.Name != null)
        {
            GetComponent<SpriteRenderer>().sprite = item.UIIcon;
            name = item.Name;
        }
    }

    private IEnumerator LockPickUp(float time)
    {
        canPickUp = false;
        yield return new WaitForSeconds(time);
        canPickUp = true;
    }
}
