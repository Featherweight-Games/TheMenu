using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreButton : MonoBehaviour {
    
    public float cooldown;
    public int score;
    public TMP_Text scoreText;

    private float cooldownTimer;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private bool CooldownExpired => cooldownTimer <= 0;

    // Start is called before the first frame update
    void Start () {
        scoreText.text = "+" + score;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable() {
        cooldownTimer = 0;
    }

    void Update() {
        cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;

        canvasGroup.alpha = CooldownExpired ? 1 : 0.5f;
    }

    public void UIResponse_Clicked() {
        if (cooldownTimer <= 0) {

            rectTransform.DOPunchScale(Vector3.one * 0.1f, 0.15f);


            int toAdd = score;
            if (GameManager.Instance.DoublePoints) {
                toAdd *= 2;
            }
            GameManager.Instance.AddScore(toAdd);
            
            cooldownTimer = cooldown;
        }
        else
        {

        }
    }
}
