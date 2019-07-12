using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class ManualOrderButton : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample3 _source;
	[SerializeField] private Button _button;

	private void Start()
	{
		_button.OnPointerClickAsObservable()
			.ThrottleFirst(TimeSpan.FromMilliseconds(500))
			.Subscribe(_ => _source.OrderEspresso())
			.AddTo(this);
	}
}