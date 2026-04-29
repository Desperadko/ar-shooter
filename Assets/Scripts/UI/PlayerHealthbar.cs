using Game.Gameplay.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerHealthbar : MonoBehaviour
    {
        [SerializeField] private PlayerHealthSystem playerHealth;
        [SerializeField] private GameObject healthContainer;
        [SerializeField] private float damagedAlpha = 0.2f;

        private Image[] healthPoints;

        private void Awake()
        {
            healthPoints = healthContainer.GetComponentsInChildren<Image>();

            Debug.Assert(
                healthPoints.Length == playerHealth.MaxHealth,
                $"Health point mismatch: {healthPoints.Length} UI elements but MaxHealth is {playerHealth.MaxHealth}"
            );
        }

        private void OnEnable()
        {
            PlayerHealthSystem.OnPlayerDamaged += UpdateDisplay;
            GameManager.OnGameStarted += ResetDisplay;
        }

        private void OnDisable()
        {
            PlayerHealthSystem.OnPlayerDamaged -= UpdateDisplay;
            GameManager.OnGameStarted -= ResetDisplay;
        }

        private void UpdateDisplay(int currentHealth)
        {
            for (int i = 0; i < healthPoints.Length; i++)
                SetAlpha(healthPoints[i], i < currentHealth ? 1f : damagedAlpha);
        }

        private void ResetDisplay()
        {
            foreach(var point in healthPoints)
            {
                SetAlpha(point, 1f);
            }
        }

        private void SetAlpha(Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
