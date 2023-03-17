using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoostButton : MonoBehaviour {

    public enum BoostType {doublePoints, haste}

    public BoostType boostType;
    public float duration;
    public float cooldown;

    private float cooldownTimer;
    private float durationTimer;
    
    private void OnEnable() {
        cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update() {
        //count down duration, then countdown cooldown
        if (durationTimer > 0) {
            durationTimer -= Time.deltaTime;
            if (durationTimer <= 0) {
                EndDuration();
            }
        } else {
            cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;
        }
    }

    public void UIResponse_Clicked() {
        if (cooldownTimer <= 0) {
            cooldownTimer = cooldown;
            durationTimer = duration;
            if (boostType == BoostType.doublePoints) {
                GameManager.Instance.EnableDoublePoints();
            } else if (boostType == BoostType.haste) {
                GameManager.Instance.EnableHaste();
            }
        }
    }

    void EndDuration() {
        if (boostType == BoostType.doublePoints) {
            GameManager.Instance.DisableDoublePoints();
        } else if (boostType == BoostType.haste) {
            GameManager.Instance.DisableHaste();
        }
    }
    
    
}
