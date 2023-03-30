using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    
    [Header("Game Screen")]
    public GameObject gameScreen;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    

    private float timer;
    private int currentScore;
    public GameObject scoreStar;
    private bool rotationrunning = false;

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
    }

    void ShowStartScreen() {
        startScreen.SetActive(true);
        gameScreen.SetActive(false);

        if (currentScore > PlayerPrefs.GetInt("highScore", 0)) {
            PlayerPrefs.SetInt("highScore", currentScore);
        }

        highScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
        
        gameState = GameState.startScreen;
    }
    
    public void AddScore(int score) {
        currentScore += score;
        scoreText.text = currentScore.ToString();

        if (!rotationrunning)
        {
            rotationrunning = true;
            var rotation = new Vector3(scoreStar.transform.rotation.x, scoreStar.transform.rotation.y+180, scoreStar.transform.rotation.z);
            scoreStar.transform.DORotate(rotation, 0.5f).OnComplete(() =>
            {
                rotationrunning = false;
                scoreStar.transform.rotation = new Quaternion(0, 0, 0, 0);
                Debug.Log("Claire end rotation");
            });
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
