using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private Button informationButton;
        
        [SerializeField] private GameObject informationCanvas;
        [SerializeField] private Button closeInformationButton;

        private void Awake()
        {
            if(informationButton != null) informationButton.onClick.AddListener(ShowInformation);
            if(closeInformationButton != null) closeInformationButton.onClick.AddListener(ShowMainMenu);
        }

        private void OnDestroy()
        {
            if (informationButton != null) informationButton.onClick.RemoveListener(ShowInformation);
            if (closeInformationButton != null) closeInformationButton.onClick.RemoveListener(ShowMainMenu);
        }

        private void ShowMainMenu() => SetCanvasActive(mainMenuCanvas);
        private void ShowInformation() => SetCanvasActive(informationCanvas);

        private void SetCanvasActive(GameObject canvas)
        {
            mainMenuCanvas.SetActive(mainMenuCanvas == canvas);
            informationCanvas.SetActive(informationCanvas == canvas);
        }
    }
}
