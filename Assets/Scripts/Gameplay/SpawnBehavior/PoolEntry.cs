using UnityEngine;
using UnityEngine.Pool;

namespace Game.Gameplay.SpawnBehavior
{
    public class PoolEntry : MonoBehaviour
    {
        public Poolable poolableType;

        private bool collectionCheck = false;
        private int initialPoolSize = 10;
        private int maxPoolSize = 100;

        public GameObject Create()
        {
            var instance = Instantiate(gameObject);
            instance.name = gameObject.name;

            return instance;
        }

        public void OnGet(GameObject gameObject) => gameObject.SetActive(true);
        public void OnRelease(GameObject gameObject) => gameObject.SetActive(false);
        public void OnDestroyPoolObject(GameObject gameObject) => Destroy(gameObject);

        public IObjectPool<GameObject> CreatePool()
        {
            return new ObjectPool<GameObject>(
                Create,
                OnGet,
                OnRelease,
                OnDestroyPoolObject,
                collectionCheck,
                initialPoolSize,
                maxPoolSize);
        }
    }

    public enum Poolable
    {
        None,

        Enemy_Slime_Blue,
        Enemy_Slime_Red,
        Enemy_Sline_Green,

        Projectile_Fire,
        Projectile_Water,
        Projectile_Nature
    }
}
