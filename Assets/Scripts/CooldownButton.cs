using UnityEngine;
using UnityEngine.UI;

public class CooldownButton : MonoBehaviour
{
    [SerializeField] protected RectTransform durationIndicator;
    [SerializeField] private Color cooldownColor = Color.red;
    [SerializeField] protected float cooldown;
    
    protected float cooldownTimer;

    private void OnEnable()
    {
        cooldownTimer = 0;
        durationIndicator.gameObject.SetActive(false);  
    }

    protected virtual void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime * GameManager.Instance.CooldownScale;   
            
            var value = cooldownTimer / cooldown;
            durationIndicator.anchorMax = new Vector2(value, 1);
        }
        else
        {
            durationIndicator.gameObject.SetActive(false);
        }
    }

    public virtual void UIResponse_Clicked() 
    {
        if (cooldownTimer <= 0) 
        {
            cooldownTimer = cooldown;

            durationIndicator.anchorMin = new Vector2(0, 0);
            durationIndicator.anchorMax = new Vector2(1, 1);
            durationIndicator.gameObject.SetActive(true);
            durationIndicator.GetComponent<Image>().color = cooldownColor;
        }
    }
}
