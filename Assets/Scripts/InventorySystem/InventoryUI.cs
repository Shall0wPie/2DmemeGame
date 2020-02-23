
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;

public class InventoryUI : MonoBehaviour
{
    public static ItemSlot draggableSlot; // Tracks currently draggable slot
    public ContainerUI mainContainerUI;
    public ContainerUI quickContainerUI;

    [SerializeField] private List<RectTransform> slots;

    private int selector;
    
    private void Update()
    {
        if (!DialogManager.instance.isInDialogue)
        {
            // Hide and displays main inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                mainContainerUI.inventoryWindow.gameObject.active =
                    mainContainerUI.inventoryWindow.gameObject.active ? false : true;
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                selector++;
                if (quickContainerUI.itemContainer.containerSize - 1 < selector)
                    selector = quickContainerUI.itemContainer.containerSize - 1;
                SelectSlot(selector);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                selector--;
                if (selector < 0)
                    selector = 0;
                SelectSlot(selector);
            }
            
            if (Input.GetKeyDown(KeyCode.G))
                quickContainerUI.DropFromSlot(selector, 1);
            if (Input.GetKeyDown(KeyCode.Q))
                quickContainerUI.UseFromSlot(selector);
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selector = 0;
                SelectSlot(selector);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selector = 1;
                SelectSlot(selector);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selector = 2;
                SelectSlot(selector);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                selector = 3;
                SelectSlot(selector);
            }
        }
    }

    private void SelectSlot(int index)
    {
        foreach (RectTransform slot in slots)
        {
            slot.GetComponent<Image>().color = Color.white;
        }
        
        slots[index].GetComponent<Image>().color = Color.green;
    }
}