using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopItemList _shopItemsHeld;
    private ShopSystem _shopSystem;

    public static UnityAction<ShopSystem, inventoryHolder> OnShopWindowRequested;

    private string id;

    public void Awake()
    {
        _shopSystem = new ShopSystem(_shopItemsHeld._items.Count, _shopItemsHeld._maxAllowedGold, _shopItemsHeld._buyMarkUp, _shopItemsHeld._sellMarkUp);

        foreach (var item in _shopItemsHeld._items)
        {
            _shopSystem.AddToShop(item.itemData, item.amount);
        }

        //id = GetComponent<UniqueID>().ID;
    }

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        var playerInv = interactor.GetComponent<inventoryHolder>();

        if (playerInv != null)
        {
            OnShopWindowRequested?.Invoke(_shopSystem, playerInv);
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
        }
    }

    public void EndInteraction()
    {

    }
}

[System.Serializable]
public class ShopSaveData
{

}

