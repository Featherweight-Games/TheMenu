using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreButton : MonoBehaviour {
    
    public float cooldown;
    public int score;
    public TMP_Text scoreText;
    public Button ButtonRef;

    public Color DisabledColor;
    public Color EnabledColor;

    private float cooldownTimer;
    private bool onCooldown;

    public Vector3 PunchScale;
    public float duration;
    
    // Start is called before the first frame update
    void Start() {
        scoreText.text = "+" + score;
    }

    private void OnEnable() {
        cooldownTimer = 0;
    }

    void Update() {
        cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;
        
        if (cooldownTimer > 0)
        {
            ButtonRef.image.color = Color.Lerp(EnabledColor, DisabledColor, cooldownTimer); // lerp between disabled and enabled
        }
        else if(onCooldown && cooldownTimer < 0)
        {
            onCooldown = false;
            //Animate punch effect when the cooldown has finished 
            ButtonRef.transform.DOScale(1.25f, 0.1f).SetEase(Ease.Flash).OnComplete(() => ButtonRef.transform.DOScale(1f, 0.1f).SetEase(Ease.Flash));
        }
    }

    public void UIResponse_Clicked() {
        if (cooldown > 0)
        {
            ButtonRef.image.color = DisabledColor;
            onCooldown = true;
        }
        if (cooldownTimer <= 0) {
            int toAdd = score;
            if (GameManager.Instance.DoublePoints) {
                toAdd *= 2;
            }
            GameManager.Instance.AddScore(toAdd);
            
            cooldownTimer = cooldown;
        }
    }
}
