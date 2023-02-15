using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot : ISerializationCallbackReceiver
{
    [SerializeField] public InventoryItemData itemData;
    [SerializeField] public int _itemID = -1;
    [SerializeField] public int stackSize;

    public InventoryItemData Data => itemData;

    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.Data)
        {
            AddToStack(invSlot.stackSize);
        }
        else
        {
            itemData = invSlot.Data;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = Data.MaxStackSize - stackSize;

        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= Data.MaxStackSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(Data, halfStack);
        return true;
    }

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1)
        {
            return;
        }

        var db = Resources.Load<Database>("Database");
        itemData = db.GetItem(_itemID);
    }
}

