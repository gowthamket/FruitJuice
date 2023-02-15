using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot inventorySlot;
    public float _dropOffset = 1f;

    private Transform _playerTransform;

    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";

        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        itemSprite.sprite = invSlot.Data.Icon;
        itemCount.text = invSlot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    public void UpdateMouseSlot()
    {

        itemSprite.sprite = inventorySlot.Data.Icon;
        itemCount.text = inventorySlot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    private void Update()
    {
        if (inventorySlot.Data != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                //if (inventorySlot.Data.ItemPrefab != null)
                //{
                //    Instantiate(inventorySlot.Data.ItemPrefab, _playerTransform.position + _playerTransform.forward * _dropOffset, Quaternion.identity);
                //}               
            }

            if (inventorySlot.StackSize > 1)
            {
                inventorySlot.AddToStack(-1);
                UpdateMouseSlot();
            }
            else
            {
                ClearSlot();
            }
        }
    }

    public void ClearSlot()
    {
        inventorySlot.ClearSlot();
        itemCount.text = "";
        itemSprite.color = Color.white;
        itemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
