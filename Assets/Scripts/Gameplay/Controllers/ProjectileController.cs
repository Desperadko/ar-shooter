using Game.Gameplay.SpawnBehavior;
using Game.Gameplay.Systems;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay.Controllers
{
    [RequireComponent(typeof(EnemyMovementSystem), typeof(PlayerAttackSystem), typeof(Collider))]
    [RequireComponent(typeof(Elemental), typeof(PoolEntry))]
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float despawnTimer;
        [SerializeField] private int startDurability = 1;
    
        [SerializeField] private EnemyMovementSystem mover;
        [SerializeField] private PlayerAttackSystem attacker;
        [SerializeField] private PoolEntry poolEntryCache;
        [SerializeField] private Elemental elemental;
        [SerializeField] private Collider _collider;

        private float timeAlive;
        private int durability;

        private bool controllerStarted;

        private void Awake()
        {
            mover = mover != null ? mover : GetComponent<EnemyMovementSystem>();
            attacker = attacker != null ? attacker : GetComponent<PlayerAttackSystem>();
            elemental = elemental != null ? elemental : GetComponent<Elemental>();
            poolEntryCache = poolEntryCache != null ? poolEntryCache : GetComponent<PoolEntry>();

            _collider = _collider != null ? _collider : GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnEnable()
        {
            timeAlive = 0f;
            durability = startDurability;

            StartController();

            GameManager.OnGameStarted += StartController;
            GameManager.OnGamePaused += StopController;
            GameManager.OnGameResumed += StartController;
            GameManager.OnGameOver += StopController;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= StartController;
            GameManager.OnGamePaused -= StopController;
            GameManager.OnGameResumed -= StartController;
            GameManager.OnGameOver -= StopController;
        }

        private void StartController()
        {
            mover.StartMoving();
            controllerStarted = true;
        }

        private void StopController()
        {
            mover.StopMoving();
            controllerStarted = false;
        }

        private void Update()
        {
            if (!controllerStarted) return;

            if(timeAlive > despawnTimer)
            {
                DestroySelf();
            }

            timeAlive += Time.deltaTime;
            mover.Move(transform.forward);
        }

        private void OnTriggerEnter(Collider other)
        {
            attacker.Attack(other.gameObject, elemental.Type);

            durability--;
            if(durability <= 0)
            {
                DestroySelf();
            }
        }

        private void DestroySelf()
        {
            if (poolEntryCache != null)
                ObjectPoolManager.Despawn(poolEntryCache);
            else
                Destroy(gameObject);
        }
    }
}