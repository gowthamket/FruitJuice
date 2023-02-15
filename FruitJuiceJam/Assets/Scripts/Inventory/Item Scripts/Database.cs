using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Inventory System / Item Data")]
public class Database : ScriptableObject
{
    [SerializeField] private List<InventoryItemData> _itemDataBase;

    [ContextMenu("Set IDs")]
    public void SetItemIDs()
    {
        _itemDataBase = new List<InventoryItemData>();

        var foundItems = Resources.LoadAll<InventoryItemData>("ItemData").OrderBy(i => i.ID).ToList();
        var hasIDInRange = foundItems.Where(i => i.ID != -1 && i.ID < foundItems.Count).OrderBy(i => i.ID).ToList();
        var hasNotIDInRange = foundItems.Where(i => i.ID != -1 && i.ID >= foundItems.Count).OrderBy(i => i.ID).ToList();
        var noID = foundItems.Where(i => i.ID <= -1).ToList();

        var index = 0;
        for (int i = 0; i < foundItems.Count; i++)
        {
            InventoryItemData itemToAdd;
            itemToAdd = hasIDInRange.Find(d => d.ID == i);

            if (itemToAdd != null)
            {
                _itemDataBase.Add(itemToAdd);
            }
            else if (index < noID.Count)
            {
                noID[index].ID = i;
                itemToAdd = noID[index];
                index++;
                _itemDataBase.Add(itemToAdd);
            }
        }

        foreach (var item in hasNotIDInRange)
        {
            _itemDataBase.Add(item);
        }
    }

    public InventoryItemData GetItem(int id)
    {
        return _itemDataBase.Find(i => i.ID == id);
    }
}

