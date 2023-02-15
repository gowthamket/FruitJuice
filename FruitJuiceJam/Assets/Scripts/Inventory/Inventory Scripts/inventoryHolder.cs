using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class inventoryHolder : MonoBehaviour
{
    [SerializeField] public int inventorySize;
    [SerializeField] public InventorySystem inventorySystem;
    [SerializeField] protected int _gold;

    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize, _gold);


    }
}

