using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ButtonStyle : MonoBehaviour
{
    public Button button;
    public RectTransform rt;
    public Image buttonBorder;
    public Image buttonFill;
    public Image burst;
    public TMPro.TextMeshProUGUI text;
    private RectTransform burstRT;
    private static float burstDur = 0.5f;
    private bool isActive = true;

    void Start() {
        if (burst != null) {
            burstRT = burst.GetComponent<RectTransform>();
            burst.color = new Color(burst.color.r, burst.color.g, burst.color.b, 0f);
        }
    }

    // public void OnPointerDown(PointerEventData eventData) {
    //     rt.DOComplete();
    //     rt.localScale = Vector3.one * 0.8f;
    // }

    public void OnClicked() {
        rt.DOComplete();
        rt.localScale = Vector3.one * 0.8f;
        rt.DOScale(Vector3.one, 0.25f).SetEase(GameManager.Instance.buttonPressCurve);
    }

    public void SetActive(bool active) {
        if (active) {
            buttonFill.color = new Color(buttonFill.color.r, buttonFill.color.g, buttonFill.color.b, 1f);
            text.color = Color.black;
        } else {
            buttonFill.color = new Color(buttonFill.color.r, buttonFill.color.g, buttonFill.color.b, 0.25f);
            text.color = Color.white;
        }

        if (active == true && isActive == false) {
            Burst(Color.white);
        }
        isActive = active;
    }

    public void SetPoweredUp(bool isPoweredUp) {
        if (isPoweredUp) {
            buttonFill.color = Color.cyan;
        } else {
            buttonFill.color = Color.white;
        }
        buttonBorder.color = buttonFill.color;
    }

    public void SetFill(float fillAmt) {
        buttonFill.fillAmount = fillAmt;
    }

    public void Burst(Color color) {
        burst.DOComplete();
        burstRT.DOComplete();
        burst.color = color;
        burstRT.sizeDelta = new Vector2(-10f, -10f);
        burstRT.DOSizeDelta(new Vector2(100f, 100f), burstDur).SetEase(Ease.OutQuad);
        burst.DOFade(0f, burstDur);
    }
}
