using Game.Gameplay.Controllers;
using Game.Gameplay.SpawnBehavior;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        [SerializeField] private Button fireProjectileButton;
        [SerializeField] private Button waterProjectileButton;
        [SerializeField] private Button natureProjectileButton;

        [SerializeField] private PoolEntry fireProjectile;
        [SerializeField] private PoolEntry waterProjectile;
        [SerializeField] private PoolEntry natureProjectile;

        private void Awake()
        {
            fireProjectileButton?.onClick.AddListener(() => SelectElement(fireProjectile, fireProjectileButton));
            waterProjectileButton?.onClick.AddListener(() => SelectElement(waterProjectile, waterProjectileButton));
            natureProjectileButton?.onClick.AddListener(() => SelectElement(natureProjectile, natureProjectileButton));

            SelectElement(fireProjectile, fireProjectileButton);
        }

        private void SelectElement(PoolEntry projectile, Button button)
        {
            playerController.SetProjectile(projectile);

            SetOutline(fireProjectileButton, false);
            SetOutline(waterProjectileButton, false);
            SetOutline(natureProjectileButton, false);
            SetOutline(button, true);
        }

        private void SetOutline(Button button, bool enabled)
        {
            if (button == null) return;
            var outline = button.GetComponent<Outline>();
            if (outline != null) outline.enabled = enabled;
        }
    }
}