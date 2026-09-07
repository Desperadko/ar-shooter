using Game.Shop;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Game.UI.Shop
{
    public class ShopUIManager : MonoBehaviour
    {
        private readonly List<SkinDefinitionCard> skinCards = new List<SkinDefinitionCard>();

        [SerializeField] private ShopManager shopManager;
        [SerializeField] private InventoryManager inventoryManager;

        [Header("Gems")]
        [SerializeField] private GemBundleDefinition[] gemBundles;
        [SerializeField] private GemBundleDefinitionCard gemBundleCardPrefab;
        [SerializeField] private Transform gemBundleContentParent;

        [Header("Skins")]
        [SerializeField] private SkinCatalog catalog;
        [SerializeField] private SkinDefinitionCard SkinCardPrefab;
        [SerializeField] private Transform skinsContentParent;

        [Header("Popup")]
        [SerializeField] private PurchaseConfirmationPopup popup;

        private void Start()
        {
            CreateGemBundleCards();
            CreateSkinCards();
        }

        private void CreateGemBundleCards()
        {
            foreach (var bundle in gemBundles)
            {
                var card = Instantiate(gemBundleCardPrefab, gemBundleContentParent);
                card.Bind(bundle, shopManager, popup);
            }
        }

        private void CreateSkinCards()
        {
            skinCards.Clear();

            foreach(var skin in catalog.Skins)
            {
                var card = Instantiate(SkinCardPrefab, skinsContentParent);
                card.Bind(skin, shopManager, inventoryManager, popup, RefreshSkinCards);
                skinCards.Add(card);
            }
        }

        private void RefreshSkinCards()
        {
            foreach (var card in skinCards)
            {
                if (card != null)
                    card.RefreshCardFunctionality();
            }
        }
    }
}
