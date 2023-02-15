using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private ShopSlot _assignedItemSlot;

    [SerializeField] private Button _addItemToCartButton;
    [SerializeField] private Button _removeItemFromCartButton;

    public ShopKeeperDisplay parentDisplay { get; private set; }

    private int _tempAmount;
    public float _markUp { get; private set; }

    private int _basketTotal;
    private int _isSelling;

    private void Awake()
    {
        _itemSprite.sprite = null;
        _itemSprite.preserveAspect = true;
        _itemSprite.color = Color.clear;
        _itemName.text = "";
        _itemCount.text = "";

        _addItemToCartButton?.onClick.AddListener(AddItemToCart);
        _removeItemFromCartButton?.onClick.AddListener(RemoveItemFromCart);
        parentDisplay = transform.parent.GetComponent<ShopKeeperDisplay>();
    }

    private void UpdateUISlot()
    {
        if (_assignedItemSlot.ItemData != null)
        {
            _itemSprite.sprite = _assignedItemSlot.ItemData.Icon;
            _itemSprite.color = Color.white;
            _itemCount.text = _assignedItemSlot.StackSize.ToString();
            var modifiedPrice = ShopKeeperDisplay.GetModifiedPrice(_assignedItemSlot.ItemData, 1, _markUp);
            _itemName.text = $"{_assignedItemSlot.ItemData.DisplayName} - {modifiedPrice}";
            _assignedItemSlot.StackSize.ToString();
        }
        else
        {
            _itemSprite.sprite = null;
            _itemSprite.color = Color.clear;
            _itemCount.text = "";
            _itemName.text = "";

        }
    }

    private void AddItemToCart()
    {
        if (_tempAmount <= 0)
        {
            return;
        }
        _tempAmount--;
        //parentDisplay.AddItemToCart(this);
        _itemCount.text = _tempAmount.ToString();
    }

    private void RemoveItemFromCart()
    {
        if (_tempAmount == _assignedItemSlot.StackSize)
        {
            return;
        }
        _tempAmount++;
        //parentDisplay.RemoveItemFromCart(this);
        _itemCount.text = _tempAmount.ToString();
    }

    public void Init(ShopSlot slot, float markUp)
    {
        _assignedItemSlot = slot;
        _markUp = markUp;
        _tempAmount = slot.StackSize;
        UpdateUISlot();
    }
}
