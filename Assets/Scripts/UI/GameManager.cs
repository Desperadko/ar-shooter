using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnInitialized;
        public static event Action OnMainMenuOpened;
        public static event Action OnGameStarted;
        public static event Action OnGamePaused;
        public static event Action OnGameResumed;
        public static event Action OnGameOver;
        public static event Action OnScan;
        public static event Action OnMinutePassed;

        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject mainMenuPersistentButtonUI;
        [SerializeField] private GameObject mainMenuRedirectionConfirmationUI;
        [SerializeField] private GameObject scanUI;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject defeatUI;

        [SerializeField] private List<Button> mainMenuButtons;
        [SerializeField] private Button ConfirmationButtonYes;
        [SerializeField] private Button ConfirmationButtonNo;

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
        
        private GameObject prevUI;
        private bool prevIsPlaying;

        private void Awake()
        {
            instance = this;

            playButton.onClick.AddListener(PlayGame);
            startButton.onClick.AddListener(StartGame);
            pauseButton.onClick.AddListener(PauseGame);
            resumeButton.onClick.AddListener(ResumeGame);
            startAgainButton.onClick.AddListener(StartGame);
            rescanButton.onClick.AddListener(Scan);

            foreach (var button in mainMenuButtons)
            {
                button.onClick.AddListener(RedirectToMainMenuConfirmation);
            }
            ConfirmationButtonYes.onClick.AddListener(PositiveConfirmation);
            ConfirmationButtonNo.onClick.AddListener(NegativeConfirmation);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(PlayGame);
            startButton.onClick.RemoveListener(StartGame);
            pauseButton.onClick.RemoveListener(PauseGame);
            resumeButton.onClick.RemoveListener(ResumeGame);
            startAgainButton.onClick.RemoveListener(StartGame);
            rescanButton.onClick.RemoveListener(Scan);

            foreach (var button in mainMenuButtons)
            {
                button.onClick.RemoveListener(RedirectToMainMenuConfirmation);
            }
            ConfirmationButtonYes.onClick.RemoveListener(PositiveConfirmation);
            ConfirmationButtonNo.onClick.RemoveListener(NegativeConfirmation);
        }

        private void Start()
        {
            mainMenuUI.SetActive(true);
            mainMenuRedirectionConfirmationUI.SetActive(false);
            scanUI.SetActive(false);
            gameUI.SetActive(false);
            pauseUI.SetActive(false);
            defeatUI.SetActive(false);
            HandlePersistentUI();

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
            mainMenuPersistentButtonUI.SetActive(true);

            isPlaying = false;

            prevUI = scanUI;
            prevIsPlaying = isPlaying;

            HandlePersistentUI();

            OnScan?.Invoke();
        }

        private void StartGame()
        {
            gameUI.SetActive(true);
            prevUI.SetActive(false);
            mainMenuPersistentButtonUI.SetActive(false);

            isPlaying = true;
            elapsedTime = 0f;
            nextMinuteMark = MINUTE;

            prevUI = gameUI;
            prevIsPlaying = isPlaying;

            HandlePersistentUI();

            OnGameStarted?.Invoke();
        }

        private void PauseGame()
        {
            pauseUI.SetActive(true);
            gameUI.SetActive(false);
            mainMenuPersistentButtonUI.SetActive(false);

            isPlaying = false;

            prevUI = pauseUI;
            prevIsPlaying = isPlaying;

            HandlePersistentUI();

            OnGamePaused?.Invoke();
        }

        private void ResumeGame()
        {
            gameUI.SetActive(true);
            pauseUI.SetActive(false);
            mainMenuPersistentButtonUI.SetActive(false);

            isPlaying = true;

            prevUI = gameUI;
            prevIsPlaying = isPlaying;

            HandlePersistentUI();

            OnGameResumed?.Invoke();
        }

        private void Scan()
        {
            scanUI.SetActive(true);
            defeatUI.SetActive(false);
            mainMenuPersistentButtonUI.SetActive(true);

            isPlaying = false;

            prevUI = scanUI;
            prevIsPlaying = isPlaying;

            HandlePersistentUI();

            OnScan?.Invoke();
        }

        private void RedirectToMainMenuConfirmation()
        {
            mainMenuRedirectionConfirmationUI.SetActive(true);
            prevUI.SetActive(false);
            mainMenuPersistentButtonUI.SetActive(false);

            isPlaying = false;

            HandlePersistentUI();
        }

        private void PositiveConfirmation()
        {
            mainMenuUI.SetActive(true);
            mainMenuRedirectionConfirmationUI.SetActive(false);
            mainMenuPersistentButtonUI.SetActive(false);

            isPlaying = false;

            prevUI = mainMenuUI;
            prevIsPlaying = isPlaying;

            HandlePersistentUI();

            OnMainMenuOpened?.Invoke();
        }

        private void NegativeConfirmation()
        {
            prevUI.SetActive(true);
            mainMenuRedirectionConfirmationUI.SetActive(false);

            isPlaying = prevIsPlaying;

            HandlePersistentUI();

            if(isPlaying)
            {
                OnGameResumed?.Invoke();
            }
        }

        public static void TriggerGameOver() => instance.GameOver();

        private void GameOver()
        {
            defeatUI.SetActive(true);
            gameUI.SetActive(false);
            HandlePersistentUI();
            mainMenuPersistentButtonUI.SetActive(false);

            isPlaying = false;

            prevUI = defeatUI;

            OnGameOver?.Invoke();
        }

        private void HandlePersistentUI()
        {
            mainMenuPersistentButtonUI.SetActive(scanUI.activeSelf);
        }
    }
}
