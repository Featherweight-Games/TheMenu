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
    
    private CoolDown _coolDown;
    private PowerupCooldown _powerupCooldown;
    
    private void OnEnable() {
        cooldownTimer = 0;
        _coolDown = this.gameObject.GetComponent<CoolDown>();
        _powerupCooldown = this.gameObject.GetComponent<PowerupCooldown>();
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

    public void UIResponse_Clicked()
    {
        if (cooldownTimer <= 0) {
            cooldownTimer = cooldown;
            durationTimer = duration;
            if (boostType == BoostType.doublePoints) {
                GameManager.Instance.EnableDoublePoints();
                _powerupCooldown.triggerPowerupDuration(duration);
            } else if (boostType == BoostType.haste) {
                GameManager.Instance.EnableHaste();
                _powerupCooldown.triggerPowerupDuration(duration);
            }
        }
    }

    void EndDuration() {
        if (boostType == BoostType.doublePoints) {
            GameManager.Instance.DisableDoublePoints();
        } else if (boostType == BoostType.haste) {
            GameManager.Instance.DisableHaste();
        }
        
        _coolDown.triggercooldown(cooldown);
    }
    
    
}
