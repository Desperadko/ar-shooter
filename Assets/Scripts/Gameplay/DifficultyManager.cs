using Game.UI;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static float EnemySpeedIncreasage { get; private set; }

    [SerializeField] private float enemySpeedIncreasage = 0.5f;

    private void OnEnable()
    {
        GameManager.OnGameStarted += ResetSpeed;
        GameManager.OnMinutePassed += IncreaseSpeed;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= ResetSpeed;
        GameManager.OnMinutePassed -= IncreaseSpeed;
    }

    private void IncreaseSpeed()
    {
        EnemySpeedIncreasage += enemySpeedIncreasage;
    }

    private void ResetSpeed()
    {
        EnemySpeedIncreasage = 0f;
    }
}
