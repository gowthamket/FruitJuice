using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI slotPrefab;

    public override void Start()
    {
        inventoryHolder.OnDynamicInventoryDisplayRequested += RefreshDynamicInventory;
        base.Start();
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        inventorySystem = invToDisplay;
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        ClearSlots();

        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (invToDisplay == null)
        {
            return;
        }

        for (int i = 0; i < invToDisplay.inventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
        }
    }

    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if (slotDictionary != null)
        {
            slotDictionary.Clear();
        }
    }
}

