using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public GameState gameState = GameState.Initialize;

    [SerializeField] PlayerController2D_TopDown player;
    [SerializeField] Timer timer;

    [SerializeField] CanvasGroup pauseMenu;
    [SerializeField] CanvasGroup gameMenu;
    [SerializeField] CanvasGroup selectLevelMenu;
    [SerializeField] SelectLevel selectLevel;

    [SerializeField] CanvasGroup gameOverMenu;

    [SerializeField] List<string> goodMessages = new List<string>();
    [SerializeField] string badMessage;
    [SerializeField] string[] endMessages = new string[2];
    [SerializeField] TextMeshProUGUI messageUI;
    [SerializeField] TextMeshProUGUI endMessageUI;
    [SerializeField] ContainerLimits containerLimits;

    bool isPaused = false;

    Coroutine currentCoroutine = null;

    [SerializeField] AnimationCurve animationCurve;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        selectLevel.Select_Level(PlayerPrefs.GetInt("level_index"));
      
        pauseMenu.CanvasGroupFade(0);
        pauseMenu.CanvasGroupInteractable(false);

        gameMenu.CanvasGroupFade(0);
        gameMenu.CanvasGroupInteractable(false);

        gameOverMenu.CanvasGroupFade(0);
        gameOverMenu.CanvasGroupInteractable(false);

        selectLevelMenu.CanvasGroupFade(1);
        selectLevelMenu.CanvasGroupInteractable(true);

        containerLimits.Init();

        yield return StartCoroutine(TransitionManager._instance.FadeOut());

        //Here we wait for the selection of the level
        yield return new WaitUntil(() => selectLevel.isLevelSelected);

        TilesetManager._instance.Init();
        yield return null;
        player.Init();

        float maxTime = .5f;

        for (float i = 0; i < maxTime; i += Time.deltaTime)
        {
            selectLevelMenu.CanvasGroupFade(animationCurve.Evaluate((maxTime - i) / maxTime));
            gameMenu.CanvasGroupFade(animationCurve.Evaluate(i / maxTime));
            yield return null;
        }
        selectLevelMenu.CanvasGroupFade(0);
        gameMenu.CanvasGroupFade(1);

        selectLevelMenu.CanvasGroupInteractable(false);
        gameMenu.CanvasGroupInteractable(true);

        timer.StartCooldown();
        gameState = GameState.Playing;
        currentCoroutine = null;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Pause(isPaused));
    }

    IEnumerator Pause(bool _)
    {
        print("Pause:" + _);
        // float maxTime = _ ? 1 : 0;
        float maxTime = .5f;

        if (_)
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

        currentCoroutine = null;
    }

    void Update()
    {
        if (gameState == GameState.Playing)
        {
            player.HandleUpdate();
            timer.UpdateCooldown();

            if (timer.isCooldownActive)
            {
                EndGame(true);
                print("Game Finished");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
    }

    public void EndGame(bool hasHurryUp = false)
    {
        gameState = GameState.GameOver;
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(End_Game(hasHurryUp));
    }

    IEnumerator End_Game(bool hasHurryUp)
    {
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

        //
        gameMenu.CanvasGroupInteractable(false);
        float maxTime = .5f;
        yield return null;

        for (float i = 0; i < maxTime; i += Time.deltaTime)
        {
            containerLimits.MoveToMid(animationCurve.Evaluate(i / maxTime));
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        for (float i = 0; i < maxTime; i += Time.deltaTime)
        {
            containerLimits.MoveToEnd(animationCurve.Evaluate(i / maxTime));
            yield return null;
        }

        //Menu Appearing
        for (float i = 0; i < maxTime; i += Time.deltaTime)
        {
            gameOverMenu.CanvasGroupFade(animationCurve.Evaluate(i / maxTime));
            yield return null;
        }
        gameOverMenu.CanvasGroupInteractable(true);
        currentCoroutine = null;
    }
}
