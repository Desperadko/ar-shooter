using Game.Shop;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Shop
{
    public class SkinDefinitionCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text skinStatusText;
        [SerializeField] private Image icon;
        [SerializeField] Button buyButton;

        private SkinDefinition skinDefinition;
        private ShopManager shopManager;
        private InventoryManager inventoryManager;
        private PurchaseConfirmationPopup popup;
        private Action onSkinChanged;

        private const string OWNED = "Owned";
        private const string EQUIPPED = "Equipped";

        public void Bind(
            SkinDefinition skinDefinition,
            ShopManager shopManager,
            InventoryManager inventoryManager,
            PurchaseConfirmationPopup popup,
            Action onSkinChanged)
        {
            this.skinDefinition = skinDefinition;
            this.shopManager = shopManager;
            this.inventoryManager = inventoryManager;
            this.popup = popup;
            this.onSkinChanged = onSkinChanged;

            icon.sprite = skinDefinition.icon;
            RefreshCardFunctionality();
        }

        public void RefreshCardFunctionality()
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => StartCoroutine(PressedAnimation()));

            var equipped = inventoryManager.GetEquippedItemId(skinDefinition.elementalType) == skinDefinition.id;

            if (equipped)
            {
                skinStatusText.text = EQUIPPED;
                buyButton.interactable = false;
                return;
            }

            buyButton.interactable = true;

            bool owned = inventoryManager.HasItem(skinDefinition.id);

            if (owned)
            {
                skinStatusText.text = OWNED;
                buyButton.onClick.AddListener(EquipSkin);
            }
            else
            {
                skinStatusText.text = skinDefinition.prices.Length > 0
                    ? skinDefinition.prices[0] + " Gems"
                    : "Undefined price";

                buyButton.onClick.AddListener(ShowPopup);
            }
        }

        private void EquipSkin()
        {
            inventoryManager.EquipItem(skinDefinition.id, skinDefinition.elementalType);
            onSkinChanged?.Invoke();
        }

        private void ShowPopup()
        {
            var price = skinDefinition.prices.Length != 0
                ? skinDefinition.prices[0].ToString().Trim()
                : "Undefined";

            price = price == "0"
                ? "Free"
                : price + " Gems";

            popup.Show($"Are you sure you want to purchase {skinDefinition.displayName} for {price}?", PurchaseSkin);
        }

        private void PurchaseSkin()
        {
            var success = shopManager.TryPurchase(skinDefinition);

            if (!success)
            {
                StartCoroutine(nameof(NotEnoughFundsAnimation));
                return;
            }

            inventoryManager.EquipItem(skinDefinition.id, skinDefinition.elementalType);
            onSkinChanged?.Invoke();
        }

        private IEnumerator NotEnoughFundsAnimation()
        {
            skinStatusText.color = Color.red;

            Vector3 start = transform.localPosition;

            for (int i = 0; i < 8; i++)
            {
                transform.localPosition = start + Vector3.right * (i % 2 == 0 ? 8f : -8f);
                yield return new WaitForSeconds(0.035f);
            }

            transform.localPosition = start;
            yield return new WaitForSeconds(1f);

            skinStatusText.color = Color.white;
        }

        private IEnumerator PressedAnimation()
        {
            yield return ShopCardFeedback.PressedAnimation(transform);
        }
    }
}
