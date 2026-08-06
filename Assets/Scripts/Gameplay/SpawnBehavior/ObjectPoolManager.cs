using Game.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Gameplay.SpawnBehavior
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private static ObjectPoolManager instance;

        private Dictionary<Poolable, IObjectPool<GameObject>> pools = new();
        private Dictionary<Poolable, HashSet<GameObject>> activeObjects = new();

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            GameManager.OnGameOver += DespawnAll;
            GameManager.OnMainMenuOpened += DespawnAll;
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= DespawnAll;
            GameManager.OnMainMenuOpened -= DespawnAll;
        }

        public static GameObject Spawn(PoolEntry poolable, Vector3 position, Quaternion rotation)
        {
            var pooled = instance.GetPoolFor(poolable).Get();
            pooled.transform.SetPositionAndRotation(position, rotation);

            instance.GetActiveSet(poolable.poolableType).Add(pooled);

            return pooled;
        }

        public static void Despawn(PoolEntry poolable)
        {
            instance.GetActiveSet(poolable.poolableType).Remove(poolable.gameObject);
            instance.GetPoolFor(poolable).Release(poolable.gameObject);
        }

        private static void DespawnAll()
        {
            foreach (var (type, set) in instance.activeObjects)
            {
                if (!instance.pools.TryGetValue(type, out var pool)) continue;

                foreach (var obj in set)
                    pool.Release(obj);

                set.Clear();
            }
        }

        private IObjectPool<GameObject> GetPoolFor(PoolEntry poolable)
        {
            if (pools.TryGetValue(poolable.poolableType, out var pool)) return pool;

            pool = poolable.CreatePool();
            pools.Add(poolable.poolableType, pool);

            return pool;
        }

        private HashSet<GameObject> GetActiveSet(Poolable type)
        {
            if (activeObjects.TryGetValue(type, out var set)) return set;

            set = new HashSet<GameObject>();
            activeObjects[type] = set;

            return set;
        }
    }
}
