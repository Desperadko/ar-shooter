using Game.Gameplay.SpawnBehavior;
using Game.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Gameplay.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float shootCooldown;
        [SerializeField] private float shootRange;
        [SerializeField] private PoolEntry projectile;

        private float lastShot;

        private void Awake()
        {
            lastShot = -shootCooldown;

            GameManager.OnInitialized += StopController;
            GameManager.OnGameStarted += StartController;
            GameManager.OnGamePaused += StopController;
            GameManager.OnGameResumed += StartController;
            GameManager.OnGameOver += StopController;
            GameManager.OnScan += StopController;
        }

        private void OnDestroy()
        {
            GameManager.OnInitialized -= StopController;
            GameManager.OnGameStarted -= StartController;
            GameManager.OnGamePaused -= StopController;
            GameManager.OnGameResumed -= StartController;
            GameManager.OnGameOver -= StopController;
            GameManager.OnScan -= StopController;
        }

        private void StartController() => enabled = true;
        private void StopController() => enabled = false;

        private void Update()
        {
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                Vector2 position = Touchscreen.current.primaryTouch.position.ReadValue();

                if (IsPointerOverUI(position)) return;

                HandleShoot(position);
            }

            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 position = Mouse.current.position.ReadValue();

                if (IsPointerOverUI(position)) return;

                HandleShoot(position);
            }
        }

        private bool IsPointerOverUI(Vector2 screenPosition)
        {
            PointerEventData eventData = new(EventSystem.current)
            {
                position = screenPosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Count > 0;
        }

        private void HandleShoot(Vector2 screenPosition)
        {
            if (Time.time < lastShot + shootCooldown) return;

            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            ObjectPoolManager.Spawn(
                projectile,
                ray.origin + ray.direction * shootRange,
                Quaternion.LookRotation(ray.direction)
            );

            lastShot = Time.time;
        }

        public void SetProjectile(PoolEntry projectile)
        {
            this.projectile = projectile;
        }
    }
}
