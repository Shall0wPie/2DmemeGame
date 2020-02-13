using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action Ejecting;

    public Text nameField { get; }
    public Image iconField { get; }
    private Stack<IItem> items = new Stack<IItem>(5);
    public int count { get; private set; }

    private Transform _draggingParent;
    private Transform _originalParent;

    public void Init(Transform draggingParent)
    {
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
    }

    public void Render(InventorySlot item)
    {
        nameField.text = item.nameField.text;
        iconField.sprite = item.iconField.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = _draggingParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (In((RectTransform) _draggingParent))
            InsertInGrid();
        else
            Eject();
    }

    public void PushItem(IItem newItem)
    {
        count++;
        items.Push(newItem);
        iconField.sprite = newItem.UIIcon;
        //icon.enabled = true;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if (count < 2)
        {
            //textCounter.enabled = false;
        }
        else
        {
            //textCounter.enabled = true;
            //textCounter.text = count.ToString();
        }
    }

    private void Eject()
    {
        Ejecting?.Invoke();
    }

    private void InsertInGrid()
    {
        int closestIndex = 0;

        for (int i = 0; i < _originalParent.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) <
                Vector3.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
            {
                closestIndex = i;
            }
        }

        transform.parent = _originalParent;
        transform.SetSiblingIndex(closestIndex);
    }

    private bool In(RectTransform draggingParent)
    {
        return draggingParent.rect.Contains(transform.localPosition);
    }

    public bool Contains(IItem item)
    {
        return items.Contains(item);
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }
}