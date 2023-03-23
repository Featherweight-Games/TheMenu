using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreButton : MonoBehaviour {
    
    public float cooldown;
    public int score;
    public TMP_Text scoreText;

    private float cooldownTimer;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField] Image cooldownFill;
    
    private bool CooldownExpired => cooldownTimer <= 0;

    // Start is called before the first frame update
    void Start () {
        scoreText.text = "+" + score;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable() {
        cooldownTimer = 0;
    }

    void Update() {
        cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;

        canvasGroup.alpha = CooldownExpired ? 1 : 0.5f;

        cooldownFill.fillAmount = cooldownTimer / cooldown;
    }

    public void UIResponse_Clicked() {
        if (cooldownTimer <= 0) {

            rectTransform.DOPunchScale(Vector3.one * 0.15f, 0.2f, 5, 5);

            int toAdd = score;
            if (GameManager.Instance.DoublePoints) {
                toAdd *= 2;
            }
            GameManager.Instance.AddScore(toAdd);
            
            cooldownTimer = cooldown;
        }
    }
}
