﻿
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public ContainerUI mainContainer;
    public ContainerUI quickContainer;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            mainContainer.inventoryWindow.gameObject.active = mainContainer.inventoryWindow.gameObject.active ? false : true;
        }
    }
}