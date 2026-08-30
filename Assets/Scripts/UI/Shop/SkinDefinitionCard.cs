using Game.Shop;
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
        [SerializeField] TMP_Text notEnoughText;

        private SkinDefinition skinDefinition;
        private ShopManager shopManager;
        private InventoryManager inventoryManager;
        private PurchaseConfirmationPopup popup;

        public void Bind(SkinDefinition skinDefinition, ShopManager shopManager, InventoryManager inventoryManager, PurchaseConfirmationPopup popup)
        {
            this.skinDefinition = skinDefinition;
            this.shopManager = shopManager;
            this.inventoryManager = inventoryManager;
            this.popup = popup;

            icon.sprite = skinDefinition.icon;

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => StartCoroutine(nameof(PressedAnimation)));
            RefreshCardFunctionality();
        }

        private void RefreshCardFunctionality()
        {
            var equipped = inventoryManager.GetEquippedItemId(skinDefinition.elementalType) == skinDefinition.id;

            if (equipped)
            {
                skinStatusText.text = "Equipped";
                buyButton.interactable = false;
                return;
            }

            buyButton.interactable = true;

            bool owned = inventoryManager.HasItem(skinDefinition.id);

            if (owned)
            {
                skinStatusText.text = "Owned";
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

        public void PurchaseSkin()
        {
            var success = shopManager.TryPurchase(skinDefinition);

            if (!success)
            {
                StartCoroutine(nameof(NotEnoughFundsAnimation));
                return;
            }

            inventoryManager.EquipItem(skinDefinition.id, skinDefinition.elementalType);
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => StartCoroutine(PressedAnimation()));
            RefreshCardFunctionality();
        }

        private IEnumerator NotEnoughFundsAnimation()
        {
            notEnoughText.gameObject.SetActive(true);
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
            notEnoughText.gameObject.SetActive(false);
        }

        private IEnumerator PressedAnimation()
        {
            Vector3 original = transform.localScale;
            Vector3 pressed = original * 0.94f;

            transform.localScale = pressed;
            yield return new WaitForSeconds(0.07f);

            transform.localScale = original;
        }
    }
}