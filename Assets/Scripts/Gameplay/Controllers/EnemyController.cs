using Game.Gameplay.Systems;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay.Controllers
{
    [RequireComponent(typeof(LookingSystem), typeof(EnemyMovementSystem))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private LookingSystem looker;
        [SerializeField] private EnemyMovementSystem mover;

        private void Awake()
        {
            looker = looker != null ? looker : GetComponent<LookingSystem>();
            mover = mover != null ? mover : GetComponent<EnemyMovementSystem>();
        }

        private void OnEnable()
        {
            StartController();

            GameManager.OnGameStarted += StartController;
            GameManager.OnGamePaused += StopController;
            GameManager.OnGameResumed += StartController;
            GameManager.OnGameOver += StopController;

            mover.UpdateSpeed(mover.Speed + DifficultyManager.EnemySpeedIncreasage);
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
        }

        private void StopController()
        {
            mover.StopMoving();
        }

        private void Update()
        {
            mover.Move(looker.LookingDirectionNormalized);
        }
    }
}
