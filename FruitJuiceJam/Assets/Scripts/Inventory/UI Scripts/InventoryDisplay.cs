using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    public virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;

        if (clickedSlot.AssignedInventorySlot.Data != null && mouseInventoryItem.inventorySlot.Data == null)
        {
            if (isShiftPressed && clickedSlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedSlot.UpdateUISlot();
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignedInventorySlot);
                clickedSlot.ClearSlot();
            }

        }

        if (clickedSlot.AssignedInventorySlot.Data == null && mouseInventoryItem.inventorySlot.Data != null)
        {
            clickedSlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.inventorySlot);
            clickedSlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }


        if (clickedSlot.AssignedInventorySlot.Data != null && mouseInventoryItem.inventorySlot.Data != null)
        {
            bool isSameItem = clickedSlot.AssignedInventorySlot.Data == mouseInventoryItem.inventorySlot.Data;

            if (clickedSlot.AssignedInventorySlot.Data == mouseInventoryItem.inventorySlot.Data &&
                clickedSlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.inventorySlot.StackSize))
            {
                clickedSlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.inventorySlot);
                clickedSlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
            }
            else if (isSameItem &&
                !clickedSlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.inventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1)
                {
                    SwapSlots(clickedSlot);
                }
                else
                {
                    int remainingOnMouse = mouseInventoryItem.inventorySlot.StackSize - leftInStack;

                    clickedSlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedSlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.inventorySlot.Data, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                }
            }

            if (clickedSlot.AssignedInventorySlot.Data != mouseInventoryItem.inventorySlot.Data)
            {
                SwapSlots(clickedSlot);
            }
        }
    }

    private void SwapSlots(InventorySlot_UI clickedSlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.inventorySlot.Data, mouseInventoryItem.inventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignedInventorySlot);


        clickedSlot.ClearSlot();
        clickedSlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedSlot.UpdateUISlot();
    }
}

