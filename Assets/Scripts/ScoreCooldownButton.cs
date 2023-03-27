using TMPro;

public class ScoreCooldownButton : CooldownButton
{
    public int score;
    public TMP_Text scoreText;

    private void Start() => scoreText.text = "+" + score;

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
        }
        
        // Handle cooldown
        base.UIResponse_Clicked();
    }
}
