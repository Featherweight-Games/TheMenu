using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCooldownButton : CooldownButton
{
    public int score;
    public TMP_Text scoreText;
    
    [SerializeField] private Color doublePointsTextColour = Color.yellow;

    [Header("Star Effect Settings")]
    [SerializeField] private RectTransform starEffect;
    [SerializeField] private Color starEffectColor = Color.yellow;
    [SerializeField] private float starMaxHeight = 80f;
    [SerializeField] private float starDuration = 0.5f;

    private float starTimer;

    private void Start() => starEffect.gameObject.SetActive(false);

    protected override void Update()
    {
        base.Update();

        scoreText.text = GameManager.Instance.DoublePoints ? $"+ {score * 2}" : $"+ {score}";
        scoreText.color = GameManager.Instance.DoublePoints ? doublePointsTextColour : Color.black;

        // Animate star
        if (starTimer > 0)
        {
            var timePercentage = starTimer / starDuration;
            var height = starMaxHeight * (1 - timePercentage);

            starEffect.GetComponent<Image>().color =
                new Color(starEffectColor.r, starEffectColor.g, starEffectColor.b, timePercentage);
            
            starEffect.anchoredPosition = new Vector2(0, height);
            
            starTimer -= Time.deltaTime;

            // Reset star when timer runs out
            if (starTimer <= 0)
            {
                starEffect.gameObject.SetActive(false);
                starEffect.anchoredPosition = new Vector2(0, 0);
                starEffect.GetComponent<Image>().color = starEffectColor;
            }
        }
    }

    public override void UIResponse_Clicked() 
    {
        if (cooldownTimer <= 0) 
        {
            var toAdd = score;
            
            if (GameManager.Instance.DoublePoints) 
            {
                toAdd *= 2;
            }
            
            GameManager.Instance.AddScore(toAdd);
            
            // Show little star effect
            starEffect.gameObject.SetActive(true);
            starTimer = starDuration;
        }
        
        // Handle cooldown
        base.UIResponse_Clicked();
    }
}
