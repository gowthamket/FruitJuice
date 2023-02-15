using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemPickup : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;

    private BoxCollider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.isTrigger = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inventory = other.transform.GetComponent<inventoryHolder>();

        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}

