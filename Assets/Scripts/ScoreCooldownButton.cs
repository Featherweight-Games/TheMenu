using TMPro;
using UnityEngine;

public class ScoreCooldownButton : CooldownButton
{
    public int score;
    public TMP_Text scoreText;
    
    [SerializeField] private Color doublePointsTextColour = Color.yellow;

    protected override void Update()
    {
        base.Update();

        scoreText.text = GameManager.Instance.DoublePoints ? $"+ {score * 2}" : $"+ {score}";
        scoreText.color = GameManager.Instance.DoublePoints ? doublePointsTextColour : Color.black;
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
        }
        
        // Handle cooldown
        base.UIResponse_Clicked();
    }
}
