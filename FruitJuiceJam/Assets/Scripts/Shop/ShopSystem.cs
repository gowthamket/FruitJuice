using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    private List<ShopSlot> _shopInventory;
    private int _availableGold;
    public float _buyMarkUp;
    private float _sellMarkUp;

    public List<ShopSlot> ShopInventory;
    public int availableGold => _availableGold;

    public float buyMarkUp => _buyMarkUp;
    public float sellMarkUp => _sellMarkUp;

    public ShopSystem(int size, int gold, float buyMarkUp, float sellMarkUp)
    {
        _availableGold = gold;
        _buyMarkUp = buyMarkUp;
        _sellMarkUp = sellMarkUp;

        SetShopSize(size);
    }

    private void SetShopSize(int size)
    {
        _shopInventory = new List<ShopSlot>(size);

        for (int i = 0; i < size; i++)
        {
            _shopInventory.Add(new ShopSlot());
        }
    }

    public void AddToShop(InventoryItemData data, int amount)
    {
        if (ContainsItem(data, out ShopSlot shopSlot))
        {
            shopSlot.AddToStack(amount);
        }

        var freeSlot = GetFreeSlot();
        freeSlot.AssignItem(data, amount);
    }

    private ShopSlot GetFreeSlot()
    {
        var freeSlot = _shopInventory.FirstOrDefault(i => i.ItemData == null);

        if (freeSlot == null)
        {
            freeSlot = new ShopSlot();
            _shopInventory.Add(freeSlot);
        }

        return freeSlot;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out ShopSlot shopSlot)
    {
        shopSlot = _shopInventory.Find(i => i.ItemData == itemToAdd);
        return shopSlot != null;
    }

    public void PurchaseItem(InventoryItemData data, int amount)
    {
        if (!ContainsItem(new InventoryItemData(), out ShopSlot slot))
        {
            return;
        }

        slot.RemoveFromStack(amount);
    }

    public void GainGold(int basketTotal)
    {
        throw new NotImplementedException();
    }

    public void SellItem(InventoryItemData kvpKey, int kvpValue, int price)
    {
        AddToShop(kvpKey, kvpValue);
        ReduceGold(price);
    }

    private void ReduceGold(int price)
    {
        _availableGold -= price;
    }
}

