using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextPing : MonoBehaviour
{

    public TMPro.TextMeshProUGUI text;
    private float dur = 2f;
    
    void Start() {
        transform.DOLocalMoveY(100f, dur).SetEase(Ease.OutQuint);
        text.DOFade(0f, dur).SetEase(Ease.OutQuint).OnComplete(() => {
            Destroy(this);
        });
    }

    public void SetText(string text) {
        this.text.text = text;
    }
}
