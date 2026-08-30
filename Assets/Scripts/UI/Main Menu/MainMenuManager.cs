using Game.UI.Shop;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private Button[] goBackButtons;
        
        [SerializeField] private GameObject informationCanvas;
        [SerializeField] private Button informationButton;

        [SerializeField] private GameObject shopCanvas;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button[] gemsButtons;

        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private Button inventoryButton;

        [SerializeField] private ShopTabNavigator shopTabNavigator;

        private GameObject currentCanvas;

        private void Awake()
        {
            currentCanvas = mainMenuCanvas;

            foreach (var button in goBackButtons)
            {
                if(button != null) button.onClick.AddListener(GoBack);
            }

            if(informationButton != null) informationButton.onClick.AddListener(ShowInformation);
            if(shopButton != null) shopButton.onClick.AddListener(OpenShop);

            foreach (var button in gemsButtons)
            {
                if (button != null) button.onClick.AddListener(OpenGemsTab);
            }
            
            if(inventoryButton != null) inventoryButton.onClick.AddListener(OpenInventory);
        }

        private void OnDestroy()
        {
            foreach (var button in goBackButtons)
            {
                if (button != null) button.onClick.RemoveListener(GoBack);
            }

            if (informationButton != null) informationButton.onClick.RemoveListener(ShowInformation);
            if (shopButton != null) shopButton.onClick.RemoveListener(OpenShop);

            foreach (var button in gemsButtons)
            {
                if (button != null) button.onClick.RemoveListener(OpenGemsTab);
            }

            if (inventoryButton != null) inventoryButton.onClick.RemoveListener(OpenInventory);
        }

        private void ShowInformation() => SetCanvasActive(informationCanvas);
        private void OpenShop() => SetCanvasActive(shopCanvas);
        private void OpenInventory() => SetCanvasActive(inventoryCanvas);
        private void GoBack() => SetCanvasActive(mainMenuCanvas);

        private void OpenGemsTab()
        {
            OpenShop();
            shopTabNavigator.ShowGemsTab();
        }

        private void SetCanvasActive(GameObject canvas)
        {
            currentCanvas.gameObject.SetActive(false);
            currentCanvas = canvas;
            currentCanvas.gameObject.SetActive(true);
        }
    }
}
