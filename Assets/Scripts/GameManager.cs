using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    enum GameState {
        startScreen,
        gameScreen
    };
    

    [Header("Start Screen")]
    public GameObject startScreen;
    public TMP_Text highScoreText;
    public CanvasGroup StartScreenCG;

    [Header("Game Screen")]
    public GameObject gameScreen;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public CanvasGroup GameScreenCG;



    [Header("Misc")]
    public GameObject TitleScreen;
    public GameObject StudioName;
    public Point PointPrefab;
    public GameObject PointPrefabParent;

    private float timer;
    private int currentScore;

    private const float GAME_DURATION = 30f;

    private GameState gameState;

    public bool DoublePoints { get; private set; } = false;
    public bool HasteEnabled { get; private set; } = false;
    public float CooldownScale { get; private set; } = 1f;
    

    public void Start() {
        ShowStartScreen();
    }

    public void Update() {
        if (timer > 0) {
            timerText.text = timer.ToString("F2") + "s";
            timer -= Time.deltaTime;
        } else {
            if (gameState == GameState.gameScreen) {
                ShowStartScreen();
            }
        }
    }

    public Point SpawnPoints()
    {
        //if (Point.PointsPool.Count > 0)
        //{
        //    return Point.PointsPool.Pop();
        //} 
        return Instantiate(PointPrefab, PointPrefabParent.transform);
    }

    void ShowGameScreen() {
        gameScreen.SetActive(true);
        TitleScreen.SetActive(false);
        StudioName.SetActive(false);


        StartScreenCG.DOFade(0, 0.15f).SetEase(Ease.OutExpo).OnComplete(() => GameScreenCG.DOFade(1, 0.35f).SetEase(Ease.InExpo));


        timer = GAME_DURATION;
        timerText.text = timer.ToString("F2");
        currentScore = 0;
        scoreText.text = currentScore.ToString();

        gameState = GameState.gameScreen;
    }

    void ShowStartScreen()
    {
        TitleScreen.SetActive(true);
        StudioName.SetActive(true);
        gameScreen.SetActive(false);
        StartScreenCG.DOFade(1, 0.75f).SetEase(Ease.InSine).OnComplete(() => TitleScreen.transform.DOLocalMoveY(225f,1.25f).SetEase(Ease.InOutElastic).OnComplete(
            () =>
            {
                StudioName.transform.DOMoveX(950f, 1f).SetEase(Ease.InExpo);
            }));

        if (currentScore > PlayerPrefs.GetInt("highScore", 0)) {
            PlayerPrefs.SetInt("highScore", currentScore);
        }

        highScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
        
        gameState = GameState.startScreen;
    }
    
    public void AddScore(int score) {
        currentScore += score;
        scoreText.text = currentScore.ToString();
        var point = SpawnPoints();
        if (point != null) // Points that fall from the sky
        {
            point.PointsGained.text = $"+{score}";
            point.DoublePoints = DoublePoints;
            point.HasteEnabled = HasteEnabled;
        }
    }

    public void EnableDoublePoints() {
        Debug.Log("Double Points - ON");
        DoublePoints = true;
    }

    public void DisableDoublePoints() {
        Debug.Log("Double Points - OFF");
        DoublePoints = false;
    }
    
    public void EnableHaste() {
        Debug.Log("HASTE - ON");
        CooldownScale = 4f;
        HasteEnabled = true;
    }

    public void DisableHaste() {
        Debug.Log("HASTE - OFF");
        CooldownScale = 1f;
        HasteEnabled = false;
    }
    
    
    //UI CALLBACKS
    public void UIResponse_StartGame() {
        ShowGameScreen();
    }
    
}
