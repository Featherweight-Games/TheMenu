using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PowerupCooldown : MonoBehaviour
{
    public Image coolDownRing;
    
    public void triggerPowerupDuration(float time)
    {
        coolDownRing.gameObject.SetActive(true);
        coolDownRing.fillAmount = 1f;

        coolDownRing.DOFillAmount(0, time).OnComplete(() =>
        {
            coolDownRing.fillAmount = 0f;
            coolDownRing.gameObject.SetActive(false);
        });
    }
}
