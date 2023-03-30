using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    public Image coolDownImage;

    public void triggercooldown(float time)
    {
        coolDownImage.gameObject.SetActive(true);
        coolDownImage.fillAmount = 1f;

        coolDownImage.DOFillAmount(0, time).OnComplete(() =>
        {
            coolDownImage.fillAmount = 0f;
            coolDownImage.gameObject.SetActive(false);
        });
    }
}
