using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameScreen : MonoBehaviour {
    public ScoreButton _scoreButtonLow;
    public ScoreButton _scoreButtonHigh;

    public UIPregame pregame;
    public RectTransform scoreRing;
    public AnimationCurve popAnim;
    public float scaleInfRate = 1.0f;

    public RectTransform spawnZoneL;
    public RectTransform spawnZoneR;

    private Coroutine addEffectRoutine = null;
    private float scaleInf = 0.0f;

    public void Start() {
        GameManager.Instance.OnAddScore += OnAddScore;

        _scoreButtonLow.OnUsed += OnScoreLowUsed;
        _scoreButtonLow.OnRestored += OnScoreLowRestored;

        _scoreButtonHigh.OnUsed += OnScoreHighUsed;
        _scoreButtonHigh.OnRestored += OnScoreHighRestored;
    }

    public IEnumerator Warmup() {
        _scoreButtonLow.gameObject.SetActive(false);
        _scoreButtonHigh.gameObject.SetActive(false);

        yield return pregame.Anim();

        _scoreButtonLow.gameObject.SetActive(true);
        _scoreButtonHigh.gameObject.SetActive(true);
    }

    public void Update() {
        scoreRing.localScale = Vector3.one + (Vector3.one * scaleInf);
        if(scaleInf > 0.0f) {
            scaleInf -= Time.deltaTime / scaleInfRate;
        } else {
            scaleInf = 0.0f;
        }
        
    }

    public void OnAddScore(int inc, int currnet) {
        scaleInf += (float)inc / 100;
        if(scaleInf > 0.5f) {
            scaleInf = 0.5f;
        }
    }

    public void OnScoreLowUsed() {
        _scoreButtonLow.Hide();
    }

    public void OnScoreLowRestored() {
        _scoreButtonLow.Show();

        var randAng = UnityEngine.Random.Range(0, Mathf.PI * 2);
        var randX = Mathf.Sin(randAng) * UnityEngine.Random.Range(0, spawnZoneR.rect.width / 2);
        var randY = Mathf.Cos(randAng) * UnityEngine.Random.Range(0, spawnZoneR.rect.height / 2);
        
        _scoreButtonLow.GetComponent<RectTransform>().anchoredPosition = new Vector3(randX, randY, 0);
    }

    
    public void OnScoreHighUsed() {
        _scoreButtonHigh.Hide();
    }

    public void OnScoreHighRestored() {
        _scoreButtonHigh.Show();

        var randAng = UnityEngine.Random.Range(0, Mathf.PI * 2);
        var randX = Mathf.Sin(randAng) * UnityEngine.Random.Range(0, spawnZoneR.rect.width / 2);
        var randY = Mathf.Cos(randAng) * UnityEngine.Random.Range(0, spawnZoneR.rect.height / 2);
        
        _scoreButtonHigh.GetComponent<RectTransform>().anchoredPosition = new Vector3(randX, randY, 0);
    }

}
