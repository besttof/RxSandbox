using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public sealed class DebugPanelPresenter : MonoBehaviour
{

	[SerializeField] private GameState _gameState;
	[SerializeField] private CanvasGroup _canvasGroup;
	
	[SerializeField] private Slider _ballsLeftSlider;
	[SerializeField] private Slider _maxActiveBallsSlider;

	private readonly ReactiveProperty<bool> _debugVisible = new ReactiveProperty<bool>();
	private void Start()
	{
		_gameState.BallsLeft.TwoWayBindTo(_ballsLeftSlider).AddTo(this);
		_gameState.MaxActiveBalls.TwoWayBindTo(_maxActiveBallsSlider).AddTo(this);

		_debugVisible.Subscribe(
			             v =>
			             {
				             _canvasGroup.DOFade(v ? 1f : 0f, 0.3f);
				             _canvasGroup.interactable = v;
			             })
		             .AddTo(this);

		Observable.EveryUpdate()
		          .Where(_ => Input.GetKeyUp(KeyCode.D))
		          .Subscribe(_ => _debugVisible.Value = !_debugVisible.Value)
		          .AddTo(this);
	}
}

public static class UIRxExtensions
{
	public static IDisposable TwoWayBindTo(this IReactiveProperty<int> source, Slider slider)
	{
//		slider.value = source.Value; // since we're accepting a reactive property as param, we could do this

		var d1 = slider.OnValueChangedAsObservable()
		               .Skip(1) // we don't want the initial value of the slider overriding the source value
		               .SubscribeWithState(source, (f, p) => p.Value = (int) f);
		var d2 = source.SubscribeWithState(slider, (i, s) => s.value = i);

		return StableCompositeDisposable.Create(d1, d2);
	}
}