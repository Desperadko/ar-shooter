using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnInitialized;
        public static event Action OnGameStarted;
        public static event Action OnGamePaused;
        public static event Action OnGameResumed;
        public static event Action OnGameOver;
        public static event Action OnScan;
        public static event Action OnMinutePassed;

        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject scanUI;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject defeatUI;

        [SerializeField] private Button playButton;
        [SerializeField] private Button startButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button startAgainButton;
        [SerializeField] private Button rescanButton;

        private static GameManager instance;

        private bool isPlaying;
        private float elapsedTime;
        private float nextMinuteMark;
        private const int MINUTE = 60;

        private void Awake()
        {
            instance = this;

            playButton.onClick.AddListener(PlayGame);
            startButton.onClick.AddListener(StartGame);
            pauseButton.onClick.AddListener(PauseGame);
            resumeButton.onClick.AddListener(ResumeGame);
            startAgainButton.onClick.AddListener(StartGame);
            rescanButton.onClick.AddListener(Scan);
        }

        private void Start()
        {
            mainMenuUI.SetActive(true);
            scanUI.SetActive(false);
            gameUI.SetActive(false);
            pauseUI.SetActive(false);
            defeatUI.SetActive(false);

            isPlaying = false;

            OnInitialized?.Invoke();
        }

        private void Update()
        {
            if (!isPlaying) return;

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= nextMinuteMark)
            {
                nextMinuteMark += MINUTE;
                OnMinutePassed?.Invoke();
            }
        }

        private void PlayGame()
        {
            scanUI.SetActive(true);
            mainMenuUI.SetActive(false);

            OnScan?.Invoke();
        }

        private void StartGame()
        {
            gameUI.SetActive(true);
            defeatUI.SetActive(false);
            scanUI.SetActive(false);

            isPlaying = true;
            elapsedTime = 0f;
            nextMinuteMark = MINUTE;

            OnGameStarted?.Invoke();
        }

        private void PauseGame()
        {
            pauseUI.SetActive(true);
            gameUI.SetActive(false);

            isPlaying = false;

            OnGamePaused?.Invoke();
        }

        private void ResumeGame()
        {
            gameUI.SetActive(true);
            pauseUI.SetActive(false);

            isPlaying = true;

            OnGameResumed?.Invoke();
        }

        private void Scan()
        {
            scanUI.SetActive(true);
            defeatUI.SetActive(false);

            isPlaying = false;

            OnScan?.Invoke();
        }

        public static void TriggerGameOver() => instance.GameOver();

        private void GameOver()
        {
            defeatUI.SetActive(true);
            gameUI.SetActive(false);

            isPlaying = false;

            OnGameOver?.Invoke();
        }
    }
}
