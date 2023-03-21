using UnityEngine;
using UnityEngine.UI;

namespace EMCTools
{
    public class UIStatBarParent : MonoBehaviour
    {
        [SerializeField] UIStatBarChild[] bars;
        float currentPercent = 1;

        public void UpdateFillPercent(float percent)
        {
            int barsToFill = Mathf.CeilToInt(Mathf.Clamp(percent, 0.0f, 1.0f) * bars.Length);

            for (int i = 0; i < bars.Length; i++)
            {
                //print(i + ", barsToFill: " + barsToFill + ", bars.Length = " + bars.Length + ", is bars[i] valid: " + (bars[i] != null));
                if (bars[i] == null) continue;
                if (i < barsToFill) bars[i].OnFill();
                else bars[i].OnEmpty();
            }

            currentPercent = percent;
        }

        public void ResizeBar(int newSize)
        {
            if (bars.Length == newSize) return;

            if (bars.Length == 0)
            {
                Debug.LogWarning("UIStatBarParent cannot resize itself, for it has no elements to begin with");
                return;
            }

            UIStatBarChild[] newBars = new UIStatBarChild[newSize];

            if (bars.Length > newSize)
            {
                for (int i = 0; i < bars.Length; i++)
                {
                    if (i < newSize) newBars[i] = bars[i];
                    else if (bars[i] != null) Destroy(bars[i].gameObject);
                }
            }
            else
            {
                for (int i = 0; i < bars.Length; i++) newBars[i] = bars[i];
                for (int i = bars.Length; i < newSize; i++)
                {
                    UIStatBarChild newBar = Instantiate(bars[0].gameObject, bars[0].transform.parent).GetComponent<UIStatBarChild>();
                    newBars[i] = newBar;
                }
            }

            bars = newBars;
            UpdateFillPercent(currentPercent);
        }
    }
}