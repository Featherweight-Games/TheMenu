using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BoostButton : MonoBehaviour {

    public enum BoostType {doublePoints, haste}

    public BoostType boostType;
    public float duration;
    public float cooldown;

    private float cooldownTimer;
    private float durationTimer;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private bool CooldownExpired => cooldownTimer <= 0;

    private void Start ()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        cooldownTimer = 0;
    }

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

        canvasGroup.alpha = CooldownExpired ? 1 : 0.5f;
    }

    public void UIResponse_Clicked()
    {
        if (CooldownExpired)
        {
            rectTransform.DOPunchScale(Vector3.one * 0.1f, 0.15f);

            cooldownTimer = cooldown;
            durationTimer = duration;

            if (boostType == BoostType.doublePoints)
            {
                GameManager.Instance.EnableDoublePoints();
            }
            else if (boostType == BoostType.haste)
            {
                GameManager.Instance.EnableHaste();
            }
        }
        else
        {

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
