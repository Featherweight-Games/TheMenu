using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager> {

    enum GameState {
        startScreen,
        gameScreen,
        transition,
    };
    
    
    [Header("Start Screen")]
    public GameObject startScreen;
    public TMP_Text highScoreText;
    
    [Header("Game Screen")]
    public UIGameScreen gameScreen;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    
    

    private float timer;
    private int currentScore;

    private const float GAME_DURATION = 30f;

    private GameState gameState;

    public bool DoublePoints { get; private set; } = false;
    public float CooldownScale { get; private set; } = 1f;

    public System.Action<int, int> OnAddScore;


    public void Start() {
        ShowStartScreen();
    }

    public void Update() {
        switch(gameState) {
            case GameState.gameScreen: {
                timerText.text = $"{(int)timer}s";
                timer -= Time.deltaTime;
                if(timer <= 0.0f) {
                    ShowStartScreen();
                }
                break;
            }
        }
    }

    void ShowGameScreen() {
        startScreen.SetActive(false);
        gameScreen.gameObject.SetActive(true);
        timer = GAME_DURATION;
        timerText.text = $"{(int)timer}s";
        currentScore = 0;
        scoreText.text = currentScore.ToString();

        gameState = GameState.transition;

        StartCoroutine(gameScreen.Warmup());
    }

    public void OnGameStart() {
        gameState = GameState.gameScreen;
    }

    void ShowStartScreen() {
        startScreen.SetActive(true);
        gameScreen.gameObject.SetActive(false);

        if (currentScore > PlayerPrefs.GetInt("highScore", 0)) {
            PlayerPrefs.SetInt("highScore", currentScore);
        }

        highScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
        
        gameState = GameState.startScreen;
    }
    
    public void AddScore(int score) {
        currentScore += score;
        scoreText.text = currentScore.ToString();

        if(OnAddScore != null) {
            OnAddScore.Invoke(score, currentScore);
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
