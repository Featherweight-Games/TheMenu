using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ScoreButton : MonoBehaviour {
    
    public float cooldown;
    public int score;
    public TMP_Text scoreText;

    public System.Action OnUsed;
    public System.Action OnRestored;

    private float cooldownTimer;
    public AnimationCurve animFade;
    public CanvasGroup cg;
    
    // Start is called before the first frame update
    void Start() {
        if(scoreText != null) {
            scoreText.text = "+" + score;
        }
    }

    private void OnEnable() {
        cooldownTimer = 0;
        if(OnRestored != null) {
            OnRestored.Invoke();
        }
    }

    public void Hide() {
        StartCoroutine(UIAnim.Lerp(0.1f, animFade, (dt) => {
            cg.alpha = 1.0f - dt;
        }));
    }

    public void Show() {
        StartCoroutine(UIAnim.Lerp(0.1f, animFade, (dt) => {
            cg.alpha = dt;
        }));
    }

    void Update() {
        if(cooldownTimer <= 0.0f) {
            return;
        }

        cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;

        if(cooldownTimer <= 0) {
            if(OnRestored != null) {
                OnRestored.Invoke();
            }
        }
    }

    public void UIResponse_Clicked() {
        if (cooldownTimer <= 0) {
            int toAdd = score;
            if (GameManager.Instance.DoublePoints) {
                toAdd *= 2;
            }
            GameManager.Instance.AddScore(toAdd);
            
            cooldownTimer = cooldown;
            if(OnUsed != null) {
                OnUsed.Invoke();
            }
        }
    }
}
