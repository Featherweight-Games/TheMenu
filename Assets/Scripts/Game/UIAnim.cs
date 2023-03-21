using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIAnim {
    public static IEnumerator Lerp(float duration, AnimationCurve curve, System.Action<float> onTick) {
        float dt = 0.0f;
        while(dt < 1.0f) {
            dt += Time.deltaTime / duration;
            onTick.Invoke(curve.Evaluate(dt));
            yield return null;
        }
        onTick.Invoke(1.0f);
        yield break;
    }
}