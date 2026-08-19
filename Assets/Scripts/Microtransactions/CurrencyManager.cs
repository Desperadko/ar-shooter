using Game.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Microtransactions
{
    public class CurrencyManager : MonoBehaviour
    {
        public event Action<int> OnCurrencyChanged;
        public int Gems => PlayerStateManager.Instance.CurrentState.currency;

        [SerializeField] private TMP_Text gemsText;
        private const string GEMS_TEXT_PREFIX = " Gems";

        private void Start()
        {
            RefreshText();
        }

        public bool CanAfford(float amount)
        {
            return amount >= 0 && amount <= Gems;
        }

        public bool TrySpend(int amount)
        {
            if (!CanAfford(amount)) return false;

            PlayerStateManager.Instance.CurrentState.currency -= amount;
            SaveAndNotify();
            return true;
        }

        public void AddGems(int amount)
        {
            if (amount <= 0)
                return;

            PlayerStateManager.Instance.CurrentState.currency += amount;
            SaveAndNotify();
        }

        private void RefreshText()
        {
            if (gemsText != null)
                gemsText.text = Gems.ToString() + GEMS_TEXT_PREFIX;
        }

        private void SaveAndNotify()
        {
            PlayerStateManager.Instance.Save();
            OnCurrencyChanged?.Invoke(Gems);
            RefreshText();
        }
    }
}
