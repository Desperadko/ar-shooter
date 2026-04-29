using Game.Gameplay.Systems;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerHurtIndicator : MonoBehaviour
    {
        [SerializeField] private Image vignetteImage;
        [SerializeField] private float flashDuration = 0.4f;
        [SerializeField] private float maxAlpha = 0.6f;

        private Coroutine flashRoutine;

        private void OnEnable()
        {
            GameManager.OnGameStarted += ResetAlpha;
            PlayerHealthSystem.OnPlayerDamaged += Flash;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= ResetAlpha;
            PlayerHealthSystem.OnPlayerDamaged -= Flash;
        }

        private void Flash(int _)
        {
            if (flashRoutine != null)
                StopCoroutine(flashRoutine);

            flashRoutine = StartCoroutine(FlashRoutine());
        }

        private void ResetAlpha()
        {
            SetAlpha(0f);
        }

        private IEnumerator FlashRoutine()
        {
            SetAlpha(maxAlpha);

            float elapsed = 0f;

            while (elapsed < flashDuration)
            {
                elapsed += Time.deltaTime;
                SetAlpha(Mathf.Lerp(maxAlpha, 0f, elapsed / flashDuration));
                yield return null;
            }

            SetAlpha(0f);
            flashRoutine = null;
        }

        private void SetAlpha(float alpha)
        {
            var color = vignetteImage.color;
            color.a = alpha;
            vignetteImage.color = color;
        }
    }
}
