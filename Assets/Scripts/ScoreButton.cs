using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreButton : MonoBehaviour {
    
    public float cooldown;
    public int score;
    public TMP_Text scoreText;

	UIButtonFeedbacks buttonFeedbacks;
	bool isCoolingDown;

    private float cooldownTimer;
    
    // Start is called before the first frame update
    void Start() {
        scoreText.text = "+" + score;
		buttonFeedbacks = GetComponent<UIButtonFeedbacks>();
    }

    private void OnEnable() {
        cooldownTimer = 0;
    }

    void Update() {
        cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;

		if(cooldownTimer <= 0 && isCoolingDown)
			buttonFeedbacks.OnCooldownEnd();

		if(isCoolingDown) {
			buttonFeedbacks.UpdateCooldownSlider(Mathf.Clamp(cooldownTimer / cooldown, 0, 1));
		}
	}

	public void UIResponse_Clicked() {
        if (cooldownTimer <= 0) {
            int toAdd = score;
            if (GameManager.Instance.DoublePoints) {
                toAdd *= 2;
            }
            GameManager.Instance.AddScore(toAdd);
			buttonFeedbacks.AnimateScorePoint(toAdd);

			isCoolingDown = cooldown > 0;
			buttonFeedbacks.OnPressed(isCoolingDown);

            cooldownTimer = cooldown;
        }
    }
}
