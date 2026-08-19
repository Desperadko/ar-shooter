using Assets.Scripts.Microtransactions;
using Game.Persistence;
using Game.Shop;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private CurrencyManager currencyManager;

    public bool TryPurchase(ShopItemDefinition item)
    {
        var itemLevel = inventoryManager.GetItemLevel(item.id);
        var price = item.GetPrice(itemLevel);

        if (!currencyManager.TrySpend(price)) return false;

        inventoryManager.AddOrUpdateItem(item.id, itemLevel + 1);

        return true;
    }

    public bool TryPurchase(GemBundleDefinition item)
    {
        //only transaction that requires 'real money'
        //here should be the logic for the actual transaction
        //since this project just shows mictotransactions as an example
        //we skip this step and directly add the item.totalAmount to the player's gems

        currencyManager.AddGems(item.TotalAmount);
        return true;
    }
}
