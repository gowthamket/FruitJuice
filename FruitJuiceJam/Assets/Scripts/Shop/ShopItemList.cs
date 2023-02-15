using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop System/Shop Item List")]
public class ShopItemList : ScriptableObject
{
    [SerializeField] public List<ShopInventoryItem> _items;
    [SerializeField] public int _maxAllowedGold;
    [SerializeField] public float _sellMarkUp;
    [SerializeField] public float _buyMarkUp;
}

[System.Serializable]
public struct ShopInventoryItem
{
    public InventoryItemData itemData;
    public int amount;
}
