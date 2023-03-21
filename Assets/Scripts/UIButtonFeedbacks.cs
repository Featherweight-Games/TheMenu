using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using EMCTools;

public class UIButtonFeedbacks : MonoBehaviour
{
	public GameObject scorePointPrefab;
	public Slider cooldownSlider;
	public RectTransform scoreTransform;

	Button button;
	RectTransform rectTransform;
	Image image;

	Tween scaleTween;
	Tween colorTween;
	UIAnimator wobbleAnimator;

	float bounceOvershoot;


	private void Start() {
		button = GetComponent<Button>();
		rectTransform = transform as RectTransform;
		image = GetComponent<Image>();
		UpdateCooldownSlider(0);
		wobbleAnimator = GetComponent<UIAnimator>();
		if (wobbleAnimator != null)
			wobbleAnimator.enabled = false;
	}

	public void OnPressed(bool hasCooldown) {
		if (hasCooldown)
			button.interactable = false;

		if(scaleTween != null && scaleTween.active) { scaleTween.Complete(); scaleTween.Kill(); }
		scaleTween = DOTween.To(() => rectTransform.localScale, x => rectTransform.localScale = x, new Vector3(1.1f, 1.1f, 1.1f), 0.1f).SetEase(Ease.OutQuad).OnComplete(() => {
			if(hasCooldown)
				OnCooldownStart();
			else
				scaleTween = DOTween.To(() => rectTransform.localScale, x => rectTransform.localScale = x, new Vector3(1, 1, 1), 0.1f).SetEase(Ease.OutQuad);
		});
	}

	void OnCooldownStart() { // Grey out, shrink slightly
		TweenColor(new Color(0.5f, 0.5f, 0.5f));
		TweenScale(new Vector3(0.9f, 0.9f, 0.9f));
		UpdateCooldownSlider(1);
	}

	public void OnCooldownEnd() {
		button.interactable = true;
		TweenColor(Color.white);
		TweenScale(new Vector3(1, 1, 1), true);
		UpdateCooldownSlider(0);
	}

	void TweenColor(Color newColor) {
		if(colorTween != null && colorTween.active)
			colorTween.Complete();
		colorTween = DOTween.To(() => image.color, x => image.color = x, newColor, 0.2f);
	}

	void TweenScale(Vector3 newScale, bool bounce = false) {
		if(scaleTween != null && scaleTween.active) {
			scaleTween.Complete();
			scaleTween.Kill();
		}
		if(bounce) {
			scaleTween = DOTween.To(() => rectTransform.localScale, x => rectTransform.localScale = x,
				new Vector3(Mathf.Pow(newScale.x, bounceOvershoot), Mathf.Pow(newScale.y, bounceOvershoot),
				Mathf.Pow(newScale.z, bounceOvershoot)), 0.2f).SetEase(Ease.OutQuad).OnComplete(() => {
					scaleTween = DOTween.To(() => rectTransform.localScale, x => rectTransform.localScale = x, newScale, 0.2f).SetEase(Ease.OutQuad);
				});
		}
		else
			scaleTween = DOTween.To(() => rectTransform.localScale, x => rectTransform.localScale = x, newScale, 0.2f).SetEase(Ease.OutQuad);
	}

	public void UpdateCooldownSlider(float percent) {
		cooldownSlider.gameObject.SetActive(percent > 0);
		cooldownSlider.value = percent;

		if (wobbleAnimator != null)
			wobbleAnimator.enabled = percent == 1;
	}

	public void AnimateScorePoint(int points) {
		GameObject instantiatedPoints = Instantiate(scorePointPrefab, transform);
		RectTransform pointTransform = instantiatedPoints.transform as RectTransform;
		pointTransform.position = instantiatedPoints.transform.position;
		pointTransform.SetParent(rectTransform.parent);
		instantiatedPoints.GetComponent<TextMeshProUGUI>().text = "" + points;
		Tween pointTween = DOTween.To(() => pointTransform.position, x => pointTransform.position = x, scoreTransform.position, 0.5f).SetEase(Ease.InOutCubic).OnComplete(() => {
			Destroy(instantiatedPoints);
		});
	}
}
