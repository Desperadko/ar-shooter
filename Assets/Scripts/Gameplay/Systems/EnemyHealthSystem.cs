using Game.Gameplay.SpawnBehavior;
using Game.UI;
using System;
using UnityEngine;

namespace Game.Gameplay.Systems
{
    [RequireComponent(typeof(PoolEntry))]
    public class EnemyHealthSystem : MonoBehaviour
    {
        public int MaxHealth => maxHealth;
        public bool IsAlive => currentHealth > 0;
        public bool IsDamaged => currentHealth < maxHealth;

        [SerializeField] private int maxHealth;
        [SerializeField] private PoolEntry cachedPoolEntry;
        
        private int currentHealth;

        public event Action<float> OnDamaged;
        public static event Action OnDeath;

        private void Awake()
        {
            cachedPoolEntry = GetComponent<PoolEntry>();
        }

        private void OnEnable()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            OnDamaged?.Invoke((float)currentHealth / maxHealth);

            if (!IsAlive)
            {
                ObjectPoolManager.Despawn(cachedPoolEntry);
                OnDeath?.Invoke();
            }
        }
    }
}
