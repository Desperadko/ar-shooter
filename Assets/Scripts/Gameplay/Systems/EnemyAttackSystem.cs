using Game.Gameplay.SpawnBehavior;
using UnityEngine;

namespace Game.Gameplay.Systems
{
    [RequireComponent(typeof(PoolEntry), typeof(Collider))]
    public class EnemyAttackSystem : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private PoolEntry cachedPoolEntry;

        private void Awake()
        {
            cachedPoolEntry = cachedPoolEntry != null ? cachedPoolEntry : GetComponent<PoolEntry>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PlayerHealthSystem>(out var health))
            {
                health.TakeDamage(damage);
                ObjectPoolManager.Despawn(cachedPoolEntry);
            }
        }
    }
}
