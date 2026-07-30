using Game.UI;
using System;
using UnityEngine;

namespace Game.Gameplay.Systems
{
    public class PlayerHealthSystem : MonoBehaviour
    {
        public static event Action<int> OnPlayerDamaged;
        public int MaxHealth => maxHealth;

        [SerializeField] private int maxHealth = 3;

        private int currentHealth;

        private void OnEnable()
        {
            GameManager.OnGameStarted += ResetHealth;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= ResetHealth;
        }

        public void TakeDamage(int damage = 1)
        {
            if (currentHealth <= 0) return;

            currentHealth = Mathf.Max(currentHealth - damage, 0);

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif

            OnPlayerDamaged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                GameManager.TriggerGameOver();
            }
        }

        public void ResetHealth()
        {
            currentHealth = maxHealth;
        }
    }
}
