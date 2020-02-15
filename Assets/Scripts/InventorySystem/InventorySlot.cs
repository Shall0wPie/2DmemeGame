using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using TMPro;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action Ejecting;

    [SerializeField] private Text nameField;
    [SerializeField] private Image iconField;
    [SerializeField] private TextMeshProUGUI counterField;
    private Stack<AssetItem> items = new Stack<AssetItem>(5);
    public int counter { get; private set; }
    bool isDragging = false;
    private Transform _draggingParent;
    private Transform _originalParent;

    private void Update()
    {

        if (Input.GetMouseButtonDown(1) && isDragging && !In((RectTransform)_draggingParent))
        {
            InventorySystem.instance.DropItem(this, 1);
            counter--;
            UpdateCounter();
        }

    }
    public void Init(Transform draggingParent, AssetItem item)
    {
      
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
        nameField.text = item.Name;
        iconField.sprite = item.UIIcon;
        Ejecting += () =>
        {
            InventorySystem.instance.DropItem(this, items.Count);
            InventorySystem.instance.slots.Remove(this);
            Destroy(gameObject);
        };
    }

    public Stack<AssetItem> GetItem()
    {
        return items;
    }
    public void Render()
    {
        nameField.text = items.Peek().Name;
        iconField.sprite = items.Peek().UIIcon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        transform.parent = _draggingParent;
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        if (In((RectTransform)_draggingParent))
            InsertInGrid();
        else
            Eject();
    }

    public void PushItem(AssetItem newItem)
    {
        counter++;
        items.Push(newItem);
        //icon.enabled = true;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if (counter == 0)
        {
            Eject();
        }
        if (counter < 2)
        {
            counterField.enabled = false;
        }
        else
        {
            counterField.enabled = true;
            counterField.text = counter.ToString();
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