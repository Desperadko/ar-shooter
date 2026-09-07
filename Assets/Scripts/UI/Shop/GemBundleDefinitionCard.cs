using Game.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Shop
{
    public class GemBundleDefinitionCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text totalAmountText;
        [SerializeField] private GameObject bonusPercentageIcon;
        [SerializeField] private TMP_Text bonusPercentageText;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Image icon;
        [SerializeField] private Button buyButton;
        [SerializeField] private GameObject bestPriceIcon;

        private GemBundleDefinition bundle;
        private ShopManager shopManager;
        private PurchaseConfirmationPopup popup;

        public void Bind(GemBundleDefinition bundle, ShopManager shopManager, PurchaseConfirmationPopup popup)
        {
            this.bundle = bundle;
            this.shopManager = shopManager;
            this.popup = popup;

            totalAmountText.text = bundle.bonusAmount > 0
                ? bundle.baseAmount.ToString() + " + " + bundle.bonusAmount.ToString()
                : bundle.baseAmount.ToString();

            if (bundle.bonusPercentage > 0)
            {
                bonusPercentageIcon.SetActive(true);
                bonusPercentageText.text = "+" + bundle.bonusPercentage.ToString() + "%";
            }
            else
            {
                bonusPercentageIcon.SetActive(false);
                bonusPercentageText.text = string.Empty;
            }

            priceText.text = bundle.simulatedPrice.ToString("0.00") + "€";

            if(bundle.bestPrice)
                bestPriceIcon.SetActive(true);

            if (icon != null)
                icon.sprite = bundle.icon;

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => StartCoroutine(ShopCardFeedback.PressedAnimation(transform)));
            buyButton.onClick.AddListener(ShowPopup);
        }

        private void ShowPopup()
        {
            var price = bundle.simulatedPrice.ToString("0.00") + "€";
            popup.Show($"Are you sure you want to purchase {bundle.displayName} for {price}?", Buy);
        }

        private void Buy()
        {
            if(shopManager != null && bundle != null)
                shopManager.TryPurchase(bundle);
        }
    }
}
