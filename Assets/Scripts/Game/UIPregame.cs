using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPregame : MonoBehaviour {

    public RectTransform time;
    public RectTransform ready;
    public RectTransform go;
    public RectTransform score;

    public AnimationCurve animCurveInOut;
    public AnimationCurve animCurveNormDist;

    public IEnumerator Anim() {
        //Setup

        time.gameObject.SetActive(false);
        ready.gameObject.SetActive(false);
        go.gameObject.SetActive(false);
        score.gameObject.SetActive(false);

        //Seq
        time.gameObject.SetActive(true);
        time.anchoredPosition = Vector3.zero;

        yield return UIAnim.Lerp(0.5f, animCurveInOut, (dt) => {
            time.GetComponent<CanvasGroup>().alpha = dt;
            time.transform.localScale = Vector3.one * (2.0f * dt);
        });

        yield return new WaitForSecondsRealtime(1.0f);

        yield return UIAnim.Lerp(0.5f, animCurveInOut, (dt) => {
            time.anchoredPosition = new Vector3(0, 450 * dt, 0);
            time.transform.localScale = Vector3.one + Vector3.one * (1.0f * (1.0f - dt));
        });

        yield return new WaitForSecondsRealtime(0.5f);

        ready.gameObject.SetActive(true);
        ready.anchoredPosition = Vector2.zero;
        yield return UIAnim.Lerp(1.0f, animCurveNormDist, (dt) => {
            ready.localScale = Vector3.one * (2 * dt);
        });
        ready.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(0.2f);

        go.gameObject.SetActive(true);
        go.anchoredPosition = Vector2.zero;
        yield return UIAnim.Lerp(1.0f, animCurveNormDist, (dt) => {
            go.localScale = Vector3.one * (2 * dt);
        });
        go.gameObject.SetActive(false);

        score.gameObject.SetActive(true);
        yield return UIAnim.Lerp(0.2f, animCurveInOut, (dt) => {
            score.GetComponent<CanvasGroup>().alpha = dt;
        });

        GameManager.Instance.OnGameStart();
    }
}
