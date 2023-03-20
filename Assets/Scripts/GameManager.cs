using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : Singleton<GameManager> {

    enum GameState {
        startScreen,
        gameScreen
    };
    
    
    [Header("Start Screen")]
    public GameObject startScreen;
    public TMP_Text highScoreText;
    
    [Header("Game Screen")]
    public GameObject gameScreen;
    public TMP_Text scoreText;
    public TMP_Text timerText;

    [Header("Animation Components")]
    public RectTransform scoreRT;
    public CanvasGroup startScreenCG;
    public CanvasGroup gameScreenCG;

    [Header("Animation Curves")]
    public AnimationCurve buttonPressCurve;

    private float timer;
    private int currentScore;

    private const float GAME_DURATION = 30f;

    private GameState gameState;

    public bool DoublePoints { get; private set; } = false;
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

    void ShowGameScreen() {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        timer = GAME_DURATION;
        timerText.text = timer.ToString("F2");
        currentScore = 0;
        scoreText.text = currentScore.ToString();

        gameState = GameState.gameScreen;
        
        gameScreenCG.DOComplete();
        gameScreenCG.DOFade(0f, 0.4f).From();
        gameScreenCG.transform.DOComplete();
        gameScreenCG.transform.DOScale(0.8f, 0.2f).From().SetEase(Ease.OutQuad);
    }

    void ShowStartScreen() {
        startScreen.SetActive(true);
        gameScreen.SetActive(false);

        if (currentScore > PlayerPrefs.GetInt("highScore", 0)) {
            PlayerPrefs.SetInt("highScore", currentScore);
        }

        highScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
        
        gameState = GameState.startScreen;

        startScreenCG.DOComplete();
        startScreenCG.DOFade(0f, 0.4f).From();
        gameScreenCG.transform.DOComplete();
        gameScreenCG.transform.DOScale(0.8f, 0.2f).From().SetEase(Ease.OutQuad);
    }
    
    public void AddScore(int score) {
        currentScore += score;
        scoreText.text = currentScore.ToString();

        scoreText.DOComplete();
        scoreText.DOColor(Color.cyan, 0.15f).From();
        scoreRT.DOComplete();
        scoreRT.DOPunchScale(Vector3.one * 1.01f, 0.15f, 2, 1f).SetEase(Ease.InBack);
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
    }

    public void DisableHaste() {
        Debug.Log("HASTE - OFF");
        CooldownScale = 1f;
    }
    
    
    //UI CALLBACKS
    public void UIResponse_StartGame() {
        ShowGameScreen();
    }
    
}
