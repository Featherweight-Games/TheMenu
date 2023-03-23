using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BoostButton : MonoBehaviour {

    public enum BoostType {doublePoints, haste}

    public BoostType boostType;
    public float duration;
    public float cooldown;

    private float cooldownTimer;
    private float durationTimer;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private RectTransform effectNameInfoRectTransform;

    [SerializeField] Image cooldownFill;
    [SerializeField] Image effectFill;


    [SerializeField] TextMeshProUGUI effectNameInfo;

    private bool CooldownExpired => cooldownTimer <= 0;

    private void Start ()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        effectNameInfoRectTransform = effectNameInfo.GetComponent<RectTransform>();
        effectNameInfoRectTransform.anchoredPosition = new Vector2(-1300, effectNameInfoRectTransform.anchoredPosition.y);
    }

    private void OnEnable()
    {
        cooldownTimer = 0;
    }

    void Update() {
        //count down duration, then countdown cooldown
        if (durationTimer > 0) {
            effectFill.fillAmount = durationTimer / duration;
            durationTimer -= Time.deltaTime;
            if (durationTimer <= 0) {
                EndDuration();
            }
        } else {
            cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;
            cooldownFill.fillAmount = cooldownTimer / cooldown;
        }

        canvasGroup.alpha =  !CooldownExpired ? 0.6f : 1f;

    }

    public void UIResponse_Clicked()
    {
        if (CooldownExpired)
        {
            rectTransform.DOShakePosition(cooldown, 7, 10, 90, false, false);

            cooldownTimer = cooldown;
            durationTimer = duration;

            effectNameInfoRectTransform.anchoredPosition = new Vector2(-1300, effectNameInfoRectTransform.anchoredPosition.y);
            Sequence seq = DOTween.Sequence();
            seq.Append(effectNameInfo.GetComponent<RectTransform>().DOAnchorPosX(-100, 0.2f));
            seq.Append(effectNameInfo.GetComponent<RectTransform>().DOAnchorPosX(100, durationTimer));
            seq.Append(effectNameInfo.GetComponent<RectTransform>().DOAnchorPosX(1300, 0.2f));

            if (boostType == BoostType.doublePoints)
            {
                GameManager.Instance.EnableDoublePoints();
            }
            else if (boostType == BoostType.haste)
            {
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
