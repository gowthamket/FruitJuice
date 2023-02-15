using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private ShopKeeperDisplay _shopKeeperDisplay;
    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequested += DisplayShopWindow;
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequested -= DisplayShopWindow;
    }

    private void DisplayShopWindow(ShopSystem shopSystem, inventoryHolder playerInventory)
    {
        _shopKeeperDisplay.gameObject.SetActive(false);
        _shopKeeperDisplay.DisplayShopWindow(shopSystem, playerInventory);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _shopKeeperDisplay.gameObject.SetActive(false);
        }
    }
}
