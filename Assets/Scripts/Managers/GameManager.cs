using System.Collections;
using System.Collections.Generic;
using AldhaDev.ExtensionMethods;
using TMPro;
using UnityEngine;

namespace AldhaDev.Managers
{
    using Timer;
    using ScriptableObjects;
    using Player.TopDown;

    public class GameManager : MonoSingleton<GameManager>
    {
        public GameState gameState = GameState.Initialize;

        [Header("Player")]
        [SerializeField] PlayerController2D_TopDown player;
    
        [Header("Timer")]
        [SerializeField] Timer timer;

        [Header("Menus")]
        [SerializeField] CanvasGroup pauseMenu;
        [SerializeField] CanvasGroup gameMenu;
        [SerializeField] SelectLevel selectLevel;

        [SerializeField] CanvasGroup gameOverMenu;

        [Header("Messages")]
        [SerializeField] List<string> goodMessages = new List<string>();
        [SerializeField] string badMessage;
        [SerializeField] string[] endMessages = new string[2];
        [SerializeField] TextMeshProUGUI messageUI;
        [SerializeField] TextMeshProUGUI endMessageUI;
    
        [SerializeField] RectTransform containerTransform;
        [SerializeField] ContainerLimitsAsset containerLimits;

        private bool _isRunning = true;

        private Coroutine _currentCoroutine;

        [SerializeField] AnimationCurve animationCurve;

        private void Start()
        {
            _currentCoroutine ??= StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            timer.Init();
            selectLevel.Select_Level(PlayerPrefs.GetInt("level_index"));

            pauseMenu.CanvasGroupFade(0);
            pauseMenu.CanvasGroupInteractable(false);

            gameMenu.CanvasGroupFade(0);
            gameMenu.CanvasGroupInteractable(false);

            gameOverMenu.CanvasGroupFade(0);
            gameOverMenu.CanvasGroupInteractable(false);

            containerTransform.TranslateToPosition(containerLimits.InitialPoint);

            Countdown.Current.InitWithFade();
            yield return StartCoroutine(TransitionManager.Current.FadeOut());
        
            //Here we wait for the selection of the level
            yield return new WaitUntil(() => selectLevel.isLevelSelected);

            TilesetManager.Current.Init();
            player.Init();

            float maxTime = .5f;

            for (float i = 0; i < maxTime; i += Time.deltaTime)
            {
                gameMenu.CanvasGroupFade(animationCurve.Evaluate(i / maxTime));
                yield return null;
            }
            gameMenu.CanvasGroupFade(1);

            gameMenu.CanvasGroupInteractable(true);
        
            yield return StartCoroutine(Countdown.Current.CountDown());
        
            gameState = GameState.Playing;
            yield return null;
            timer.StartTimer();
            _currentCoroutine = null;
        }

        public void PauseGame()
        {
            _isRunning = !_isRunning;
            _currentCoroutine ??= StartCoroutine(Pause(_isRunning));
        }

        private IEnumerator Pause(bool _)
        {
            const float maxTime = .5f;

            timer.TogglePause(_);
            if (!_)
            {
                gameState = GameState.Paused;

                for (float i = 0; i < maxTime; i += Time.deltaTime)
                {
                    pauseMenu.CanvasGroupFade(animationCurve.Evaluate(i / maxTime));
                    yield return null;
                }
                pauseMenu.CanvasGroupFade(1);
                pauseMenu.CanvasGroupInteractable(true);
            }
            else
            {
                for (float i = maxTime; i > 0; i -= Time.deltaTime)
                {
                    pauseMenu.CanvasGroupFade(animationCurve.Evaluate(i / maxTime));
                    yield return null;
                }
                pauseMenu.CanvasGroupFade(0);
                pauseMenu.CanvasGroupInteractable(false);
                gameState = GameState.Playing;
            }

            _currentCoroutine = null;
        }

        private void Update()
        {
            if (gameState == GameState.Playing)
            {
                player.HandleUpdate();
                timer.UpdateTimer();

                if (timer.hasFinished)
                    EndGame(true);

                if (Input.GetKeyDown(KeyCode.Escape))
                    PauseGame();
            }
        }

        public void EndGame(bool hasHurryUp = false)
        {
            gameState = GameState.GameOver;
            _currentCoroutine ??= StartCoroutine(End_Game(hasHurryUp));
        }

        IEnumerator End_Game(bool hasHurryUp)
        {
            timer.TogglePause(true);

            if (hasHurryUp)
            {
                messageUI.text = badMessage;
                endMessageUI.text = endMessages[0];
            }
            else
            {
                string temp = goodMessages[Random.Range(0, goodMessages.Count)];
                messageUI.text = temp;
                endMessageUI.text = temp;
            }

            gameMenu.CanvasGroupInteractable(false);
            const float maxTime = .5f;
            yield return null;

            for (float i = 0; i < maxTime; i += Time.deltaTime)
            {
                containerTransform.TranslateLerp_Vector3(containerLimits.InitialPoint,containerLimits.MidPoint,i/maxTime,animationCurve);
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            // Make an animation here

            for (float i = 0; i < maxTime; i += Time.deltaTime)
            {
                containerTransform.TranslateLerp_Vector3(containerLimits.MidPoint,containerLimits.EndPoint,i/maxTime,animationCurve);
                yield return null;
            }

            //Menu Appearing
            for (float i = 0; i < maxTime; i += Time.deltaTime)
            {
                gameOverMenu.CanvasGroupFade(animationCurve.Evaluate(i / maxTime));
                yield return null;
            }
            gameOverMenu.CanvasGroupInteractable(true);
            _currentCoroutine = null;
        }
    }
}
