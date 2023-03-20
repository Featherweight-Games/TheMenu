using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScoreButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  {
    
    public float cooldown;
    public int score;
    public TMP_Text scoreText;

    private float cooldownTimer;
    private ButtonStyle buttonStyle;

    public TextPing textPingPrefab;

    // Start is called before the first frame update
    void Start() {
        scoreText.text = "+" + score;
        buttonStyle = GetComponent<ButtonStyle>();
    }

    private void OnEnable() {
        cooldownTimer = 0;
    }

    void Update() {
        cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;
        buttonStyle.SetActive(cooldownTimer <= 0);
        buttonStyle.SetFill(Mathf.Clamp01(1f - cooldownTimer / cooldown));
    }

    public void UIResponse_Clicked() {
        if (cooldownTimer <= 0) {
            int toAdd = score;
            if (GameManager.Instance.DoublePoints) {
                toAdd *= 2;
            }
            GameManager.Instance.AddScore(toAdd);
            buttonStyle.Burst(Color.white);
            cooldownTimer = cooldown;
            
            TextPing textPing = GameObject.Instantiate(textPingPrefab, transform.position, Quaternion.identity, transform);
            textPing.SetText($"+{score}");
        }


    }
    
    public void OnPointerDown(PointerEventData eventData) {

    }

    public void OnPointerUp(PointerEventData eventData) {
        if (cooldownTimer > 0) {
            return;
        }
        buttonStyle.OnClicked();
    }
}
