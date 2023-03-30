using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PowerupCooldown : MonoBehaviour
{
    public Image coolDownRing;
    public GameObject colouredText;
    
    public void triggerPowerupDuration(float time)
    {
        coolDownRing.gameObject.SetActive(true);
        coolDownRing.fillAmount = 1f;

        coolDownRing.DOFillAmount(0, time).OnComplete(() =>
        {
            coolDownRing.fillAmount = 0f;
            coolDownRing.gameObject.SetActive(false);
        });
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(colouredText.transform.DOMoveX(1250, 0.75f));
        sequence.Append(colouredText.transform.DOShakeScale(1, 1, 1, 35, true, ShakeRandomnessMode.Full));
        sequence.Append(colouredText.transform.DOMoveX(2800, 0.75f));
        sequence.OnComplete(() =>
        {
            colouredText.transform.position = new Vector3(-1400, colouredText.transform.position.y,0);
        });
        sequence.Play();
    }
}
