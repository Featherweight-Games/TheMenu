using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using EMCTools;

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

	Tween scoreScaleTween;
	UIAnimator timerAnimator;

    private float timer;
    private int currentScore;

    private const float GAME_DURATION = 30f;

    private GameState gameState;

    public bool DoublePoints { get; private set; } = false;
    public float CooldownScale { get; private set; } = 1f;
    

    public void Start() {
        ShowStartScreen();
		timerAnimator = timerText.GetComponent<UIAnimator>();
	}

    public void Update() {
        if (timer > 0) {
            timerText.text = timer.ToString("F2") + "s";
            timer -= Time.deltaTime;

			if(timer <= 10 && !timerAnimator.enabled) {
				timerAnimator.enabled = true;
				timerText.color = Color.red;
			}
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

		if(timerAnimator == null)
			return;
		timerAnimator.enabled = false;
		timerText.color = Color.white;
	}

	void ShowStartScreen() {
        startScreen.SetActive(true);
        gameScreen.SetActive(false);

        if (currentScore > PlayerPrefs.GetInt("highScore", 0)) {
            PlayerPrefs.SetInt("highScore", currentScore);
        }

        highScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
        
        gameState = GameState.startScreen;

		if(timerAnimator == null)
			return;
		timerAnimator.enabled = false;
		timerText.color = Color.white;
	}

	public void AddScore(int score) {
        currentScore += score;
		StartCoroutine(UpdateScoreDelayed(currentScore.ToString()));
	}

	IEnumerator UpdateScoreDelayed(string newText) {
		yield return new WaitForSeconds(0.5f);
		scoreText.text = newText;
		RectTransform scoreTransform = scoreText.transform as RectTransform;

		if(scoreScaleTween != null && scoreScaleTween.active) {
			scoreScaleTween.Complete();
			scoreScaleTween.Kill();
		}
		scoreScaleTween = DOTween.To(() => scoreTransform.localScale, x => scoreTransform.localScale = x, new Vector3(1.25f, 1.25f, 1.25f), 0.1f).SetEase(Ease.OutQuad).OnComplete(() => {
			scoreScaleTween = DOTween.To(() => scoreTransform.localScale, x => scoreTransform.localScale = x, new Vector3(1, 1, 1), 0.1f).SetEase(Ease.OutQuad);
		});
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
