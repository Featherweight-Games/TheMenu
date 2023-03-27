using UnityEngine;
using UnityEngine.UI;

public class BoostCooldownButton : CooldownButton 
{
    public enum BoostType {doublePoints, haste}
    
    [SerializeField] private Color durationColor = Color.green;

    public BoostType boostType;
    public float duration;

    private float durationTimer;
    
    private void OnEnable() => durationTimer = 0;

    protected override void Update()
    {
        // Count down duration, then countdown cooldown
        if (durationTimer > 0) 
        {
            durationTimer -= Time.deltaTime;
            
            var value = durationTimer / duration;
            durationIndicator.anchorMax = new Vector2(value, 1);
            
            if (durationTimer <= 0)
            {
                EndDuration();
            }
        } 
        else 
        {
            base.Update();
        }
    }

    public override void UIResponse_Clicked() 
    {
        // Handle duration
        if (cooldownTimer <= 0 && durationTimer <= 0) 
        {
            durationTimer = duration;
            
            GetComponent<Button>().interactable = false;
            
            durationIndicator.GetComponent<Image>().color = durationColor;
            durationIndicator.gameObject.SetActive(true);
            
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

    private void EndDuration() 
    {
        if (boostType == BoostType.doublePoints) 
        {
            GameManager.Instance.DisableDoublePoints();
        } 
        else if (boostType == BoostType.haste) 
        {
            GameManager.Instance.DisableHaste();
        }
        
        // Handle cooldown
        base.UIResponse_Clicked(); 
    }
}