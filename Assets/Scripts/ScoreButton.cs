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
    private CoolDown _coolDown;
    
    // Start is called before the first frame update
    void Start() {
        scoreText.text = "+" + score;
    }

    private void OnEnable() {
        cooldownTimer = 0;
        _coolDown = this.gameObject.GetComponent<CoolDown>();
    }

    void Update() {
        cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;
    }

    public void UIResponse_Clicked() {
        if (cooldownTimer <= 0) {
            int toAdd = score;
            if (GameManager.Instance.DoublePoints) {
                toAdd *= 2;
            }
            GameManager.Instance.AddScore(toAdd);
            
            cooldownTimer = cooldown;
            if (cooldown > 0f)
            {
                _coolDown.triggercooldown(cooldown);
            }
        }
    }
}
