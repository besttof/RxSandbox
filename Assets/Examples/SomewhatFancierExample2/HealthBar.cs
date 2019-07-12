using UniRx;
using UnityEngine;
using DG.Tweening;
using System;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample2 _source;

	[SerializeField] private RectTransform _currentHealthBar;
	[SerializeField] private RectTransform _slowHealthBar;

	private void Start()
	{
		_source.NormalizedHealth
			.Subscribe(n =>	TweenBarTo(_currentHealthBar, n))
			.AddTo(this);

		_source.NormalizedHealth
			.Throttle(TimeSpan.FromMilliseconds(500))
			.Subscribe(n => TweenBarTo(_slowHealthBar, n, Ease.OutBounce))
			.AddTo(this);
	}

	private void TweenBarTo(RectTransform bar, float fill, Ease ease = Ease.OutCirc)
	{
		bar.DOAnchorMax(new Vector2(fill, 1f), 1f)
			.SetSpeedBased()
			.SetEase(ease)
			.SetId(this);
	}
}