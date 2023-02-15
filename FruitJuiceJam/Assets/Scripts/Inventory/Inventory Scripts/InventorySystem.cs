using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    [SerializeField] public int _gold;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public int inventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        _gold = 0;
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public InventorySystem(int size, int gold)
    {
        _gold = gold;
    }

    private void CreateInventory(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot))
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }

        }
        else if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.Data == itemToAdd).ToList();

        return invSlot == null ? false : true;

    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.Data == null);
        return freeSlot == null ? false : true;
    }

    public bool CheckInventoryRemaining(Dictionary<InventoryItemData, int> shoppingCart)
    {
        var clonedSystem = new InventorySystem(this.inventorySize);

        for (int i = 0; i < inventorySize; i++)
        {
            // clonedSystem.InventorySlots[i].AssignItem(this.InventorySlots[i].itemData, this.InventorySlots[i].StackSize);
        }

        foreach (var kvp in shoppingCart)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                if (!clonedSystem.AddToInventory(kvp.Key, 1))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void SpendGold(int basketTotal)
    {
        _gold -= basketTotal;
    }

    public void GainGold(int price)
    {
        _gold += price;
    }

    public void RemoveItemsFromInventory(InventoryItemData data, int amount)
    {
        if (ContainsItem(data, out List<InventorySlot> invSlot))
        {
            foreach (var slot in invSlot)
            {
                var stackSize = slot.StackSize;

                if (stackSize > amount)
                {
                    slot.RemoveFromStack(amount);
                }
                else
                {
                    slot.RemoveFromStack(stackSize);
                    amount -= stackSize;
                }

                OnInventorySlotChanged?.Invoke(slot);
            }
        }
    }

    public Dictionary<InventoryItemData, int> GetAllItemsHeld()
    {
        var distinctItems = new Dictionary<InventoryItemData, int>();

        foreach (var item in inventorySlots)
        {
            if (item.Data == null)
            {
                continue;
            }


            if (!distinctItems.ContainsKey(item.Data))
            {
                distinctItems.Add(item.Data, item.StackSize);
            }
            else
            {
                distinctItems[item.Data] += item.StackSize;
            }
        }

        return distinctItems;
    }
}
