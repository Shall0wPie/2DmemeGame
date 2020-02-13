using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action Ejecting;

    public Text nameField { get; private set; }
    public Image iconField { get; private set; }
    private Stack<AssetItem> items = new Stack<AssetItem>(5);
    public int count { get; private set; }

    private Transform _draggingParent;
    private Transform _originalParent;

    public void Init(Transform draggingParent)
    {
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
    }

    public void Render()
    {
        nameField.text = items.Peek().Name;
        iconField.sprite = items.Peek().UIIcon;
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

    public void PushItem(AssetItem newItem)
    {
        count++;
        items.Push(newItem);
        Debug.Log(newItem.UIIcon);
        iconField.sprite = newItem.UIIcon;
        Debug.Log(newItem.Name);
        nameField.text = newItem.Name;
        Debug.Log("da");
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

    public bool Contains(AssetItem item)
    {
        return items.Contains(item);
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }
}