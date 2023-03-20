using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BoostButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public enum BoostType {doublePoints, haste}

    public BoostType boostType;
    public float duration;
    public float cooldown;

    private float cooldownTimer;
    private float durationTimer;
    private ButtonStyle buttonStyle;

    private bool isBoostActive = false;
    
    private void OnEnable() {
        cooldownTimer = 0;
        buttonStyle = GetComponent<ButtonStyle>();
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
        
        buttonStyle.SetActive(cooldownTimer <= 0);
        if (isBoostActive) {
            buttonStyle.SetFill(1f);
        } else {
            buttonStyle.SetFill(Mathf.Clamp01(1f - cooldownTimer / cooldown));
        }
    }

    public void UIResponse_Clicked() {
        if (cooldownTimer <= 0) {
            cooldownTimer = cooldown;
            durationTimer = duration;
            buttonStyle.SetPoweredUp(true);
            buttonStyle.Burst(Color.cyan);
            isBoostActive = true;
            if (boostType == BoostType.doublePoints) {
                GameManager.Instance.EnableDoublePoints();
            } else if (boostType == BoostType.haste) {
                GameManager.Instance.EnableHaste();
            }
        }
    }

    void EndDuration() {
        isBoostActive = false;
        if (boostType == BoostType.doublePoints) {
            GameManager.Instance.DisableDoublePoints();
        } else if (boostType == BoostType.haste) {
            GameManager.Instance.DisableHaste();
        }
        buttonStyle.SetPoweredUp(false);
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
