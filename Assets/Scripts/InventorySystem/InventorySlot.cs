using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Random = UnityEngine.Random;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Text nameField;
    [SerializeField] private Image iconField;
    [SerializeField] private TextMeshProUGUI counterField;
    public ItemSlot itemSlot;

    private bool isDragging;
    private ContainerUI parentContainerUI;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isDragging)
        {
            ContainerUI targetContainer = OverlapContainer();
            if (targetContainer == null)
            {
                DropItems(1);
                itemSlot.TakeItem();
            }
            else
            {
                targetContainer.itemContainer.AddToContainer(itemSlot.item);
                itemSlot.TakeItem();
            }

            UpdateCounter();
        }
    }

    public void Init(ContainerUI draggingParent, ItemSlot slot)
    {
        parentContainerUI = draggingParent;
        //originalParent = transform.parent;
        nameField.text = slot.item.Name;
        iconField.sprite = slot.item.UIIcon;
        itemSlot = slot;
        UpdateCounter();
        // OnEjecting += () =>
        // {
        //     InventorySystem.instance.DropItem(this, items.Count);
        //     DestroySlot();
        // };
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryUI.dragingSlot = itemSlot;
        isDragging = true;
        transform.parent = parentContainerUI.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventoryUI.dragingSlot = null;
        isDragging = false;

        ContainerUI targetContainer = OverlapContainer();
        if (targetContainer != null)
            InsertInGrid(targetContainer);
        else if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            DropItems(itemSlot.count);
            DestroySlot();
        }
    }

    public void PushItem(AssetItem newItem)
    {
        //icon.enabled = true;
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        if (itemSlot.count <= 0)
        {
            DestroySlot();
        }

        if (itemSlot.count < 2)
            counterField.enabled = false;
        else
        {
            counterField.enabled = true;
            counterField.text = itemSlot.count.ToString();
        }
    }

    private void DropItems(int count)
    {
        Inventory caster = parentContainerUI.itemContainer.GetComponentInParent<Inventory>();
        Vector2 velocity = new Vector2(Random.Range(20f, 25f) * caster.transform.parent.lossyScale.x, 0);
        caster.DropItem(itemSlot.item, count, velocity);
    }

    private void DestroySlot()
    {
        parentContainerUI.itemContainer.RemoveFromContainer(itemSlot);
        Destroy(gameObject);
    }

    private void InsertInGrid(ContainerUI targetContainer)
    {
        // int closestIndex = 0;
        //
        // for (int i = 0; i < targetContainer.content.transform.childCount; i++)
        // {
        //     if (Vector3.Distance(transform.position, targetContainer.content.transform.GetChild(i).position) <
        //         Vector3.Distance(transform.position, targetContainer.content.transform.GetChild(closestIndex).position))
        //     {
        //         closestIndex = i;
        //     }
        // }

        transform.parent = targetContainer.content.transform;

        targetContainer.ReArrange();
        //transform.SetSiblingIndex(closestIndex);
    }

    private ContainerUI OverlapContainer()
    {
        List<RaycastResult> hited = new List<RaycastResult>();
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;


        EventSystem.current.RaycastAll(pointer, hited);

        foreach (RaycastResult hit in hited)
        {
            if (hit.gameObject.CompareTag("Container"))
            {
                return hit.gameObject.GetComponentInParent<ContainerUI>();
            }
        }

        return null;
    }
}