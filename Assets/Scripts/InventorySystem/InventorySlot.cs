using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action OnEjecting;

    [SerializeField] private Text nameField;
    [SerializeField] private Image iconField;
    [SerializeField] private TextMeshProUGUI counterField;
    public ItemSlot ItemSlot;
    public int counter { get; private set; }
    private bool isDragging;
    private Transform draggingParent;
    private Transform originalParent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isDragging && In() == null)
        {
            InventorySystem.instance.DropItem(this, 1);
            counter--;
            UpdateCounter();
        }
    }

    public void Init(Transform draggingParent, ItemSlot slot)
    {
        this.draggingParent = draggingParent;
        originalParent = transform.parent;
        nameField.text = slot.item.Name;
        iconField.sprite = slot.item.UIIcon;
        ItemSlot = slot;
        // OnEjecting += () =>
        // {
        //     InventorySystem.instance.DropItem(this, items.Count);
        //     DestroySlot();
        // };
    }

    public void DestroySlot()
    {
        InventorySystem.instance.slots.Remove(this);
        Destroy(gameObject);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        transform.parent = draggingParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        ContainerUI targetContainer = In();
        if (targetContainer != null)
            InsertInGrid(targetContainer);
        else if (eventData.button.Equals(PointerEventData.InputButton.Left))
            Eject();
    }

    public void PushItem(AssetItem newItem)
    {
        counter++;
        //icon.enabled = true;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if (counter <= 0)
        {
            Eject();
        }

        if (counter < 2)
            counterField.enabled = false;
        else
        {
            counterField.enabled = true;
            counterField.text = counter.ToString();
        }
    }

    private void Eject()
    {
        OnEjecting?.Invoke();
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

    private ContainerUI In()
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