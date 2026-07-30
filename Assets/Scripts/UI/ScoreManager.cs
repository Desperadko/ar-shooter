using Game.Gameplay.Systems;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        private int score;

        private const string SCORE_TEXT = "Score: ";

        private void OnEnable()
        {
            GameManager.OnGameStarted += ResetScore;
            EnemyHealthSystem.OnDeath += AddScore;
        }
        private void OnDisable()
        {
            GameManager.OnGameStarted -= ResetScore;
            EnemyHealthSystem.OnDeath -= AddScore;
        }

        private void ResetScore()
        {
            score = 0;
            scoreText.text = SCORE_TEXT + score.ToString();
        }

        private void AddScore()
        {
            score++;
            scoreText.text = SCORE_TEXT + score.ToString();
        }
    }
}
