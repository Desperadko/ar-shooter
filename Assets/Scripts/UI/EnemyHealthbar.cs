using Game.Gameplay.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class EnemyHealthbar : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private EnemyHealthSystem healthSystem;
        [SerializeField] private CanvasGroup canvasGroup;

        private Transform cameraPosition;
        
        private void Awake()
        {
            cameraPosition = Camera.main.transform;

            canvasGroup = canvasGroup != null ? canvasGroup : GetComponent<CanvasGroup>();
            healthSystem = healthSystem != null ? healthSystem : GetComponentInParent<EnemyHealthSystem>();

            HideHealthbar();
        }

        private void OnEnable()
        {
            healthSystem.OnDamaged += OnDamaged;
            HideHealthbar();
        }

        private void OnDisable()
        {
            healthSystem.OnDamaged -= OnDamaged;
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + cameraPosition.forward);
        }

        private void OnDamaged(float healthNormalized)
        {
            ShowHealthbar();
            fill.fillAmount = healthNormalized;
        }

        private void ShowHealthbar()
        {
            canvasGroup.alpha = 1f;
        }

        private void HideHealthbar()
        {
            fill.fillAmount = 1f;
            canvasGroup.alpha = 0f;
        }
    }
}
