
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static ItemSlot draggableSlot; // Tracks currently draggable slot
    public ContainerUI mainContainer;
    public ContainerUI quickContainer;
    
    private void Update()
    {
        // Hide and displays main inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            mainContainer.inventoryWindow.gameObject.active = mainContainer.inventoryWindow.gameObject.active ? false : true;
        }
    }
}