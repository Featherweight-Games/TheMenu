using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class BoostButton : MonoBehaviour {

    public enum BoostType {doublePoints, haste}

    public BoostType boostType;
    public float duration;
    public float cooldown;

    private float cooldownTimer;
    private float durationTimer;

    public Button ButtonRef;
    private bool OnCooldown;

    public Color ActiveColor;
    public Color DisabledColor;
    public Color ReadyToUseColor;


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
            if(OnCooldown) ButtonRef.image.color = Color.Lerp(ReadyToUseColor, DisabledColor, cooldownTimer);
            if (OnCooldown && cooldownTimer < 0)
            {
                OnCooldown = false;
                //punch effect when cooldown finished
                ButtonRef.transform.DOScale(1.25f, 0.1f).SetEase(Ease.Flash).OnComplete(() => ButtonRef.transform.DOScale(1f, 0.1f).SetEase(Ease.Flash));
            }
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
            ButtonRef.image.color = ActiveColor; // set enabled
        }
    }

    void EndDuration() {
        if (boostType == BoostType.doublePoints) {
            GameManager.Instance.DisableDoublePoints();
        } else if (boostType == BoostType.haste) {
            GameManager.Instance.DisableHaste();
        }
        ButtonRef.image.color = DisabledColor; // set disabled 
        OnCooldown = true;
    }
    
    
}
