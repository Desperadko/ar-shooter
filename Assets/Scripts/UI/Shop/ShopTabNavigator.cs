using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI.Shop
{
    public class ShopTabNavigator : MonoBehaviour
    {
        [SerializeField] private TabButtonContentPair[] tabButtonContentPairs;
        [SerializeField] private int defaultTabIndex;
        [SerializeField] private int gemsTabIndex;

        private readonly List<TabButtonListener> registeredListeners = new List<TabButtonListener>();

        private void Awake()
        {
            foreach (var pair in tabButtonContentPairs)
            {
                if (pair.TabButton == null || pair.Content == null) continue;

                var content = pair.Content;
                UnityAction listener = () => ShowContent(content);
                pair.TabButton.onClick.AddListener(listener);
                registeredListeners.Add(new TabButtonListener(pair.TabButton, listener));
            }
        }

        private void Start()
        {
            if (tabButtonContentPairs.Length == 0) return;

            defaultTabIndex = Mathf.Clamp(defaultTabIndex, 0, tabButtonContentPairs.Length - 1);
            ShowContent(tabButtonContentPairs[defaultTabIndex].Content);
        }

        private void OnDestroy()
        {
            foreach (var listener in registeredListeners)
            {
                if (listener.Button != null) listener.Button.onClick.RemoveListener(listener.Action);
            }

            registeredListeners.Clear();
        }

        public void ShowGemsTab()
        {
            if (tabButtonContentPairs.Length == 0 && gemsTabIndex >= 0 && gemsTabIndex < tabButtonContentPairs.Length) return;

            ShowContent(tabButtonContentPairs[gemsTabIndex].Content);  //controlled by the inspector
        }

        private void ShowContent(GameObject content)
        {
            foreach (var pair in tabButtonContentPairs)
            {
                if (pair.Content == null) continue;

                pair.Content.SetActive(pair.Content == content);

                if (pair.TabButton != null)
                {
                    pair.TabButton.interactable = pair.Content != content;
                }
            }
        }

        private readonly struct TabButtonListener
        {
            public readonly Button Button;
            public readonly UnityAction Action;

            public TabButtonListener(Button button, UnityAction action)
            {
                Button = button;
                Action = action;
            }
        }
    }

    [Serializable]
    public class TabButtonContentPair
    {
        public Button TabButton;
        public GameObject Content;
    }
}
