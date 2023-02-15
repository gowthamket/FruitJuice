using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI_Controller : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;

    private void OnEnable()
    {
        inventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    private void OnDisable()
    {
        inventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            DisplayInventory(new InventorySystem(30));
        }
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
}

