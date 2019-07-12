using DG.Tweening;
using UniRx;
using UnityEngine;

public class ProgressProp : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample1 _source;
	[SerializeField] private float _min;
	[SerializeField] private float _max;

	private void Start()
	{
		_source.PumaProgress
			.Select(p => p >= _min && p <= _max)
			.DistinctUntilChanged()
			.Select(i => i ? 1f : 0f)
			.Subscribe(s =>
			{
				DOTween.Complete(this);

				transform.DOScale(s, 2f)
					.SetEase(Ease.OutElastic)
					.SetId(this);

				transform.DOShakeRotation(2f, new Vector3(0, 0, 45f), 8, 45)
					.SetId(this);
			})
			.AddTo(this);
	}
}