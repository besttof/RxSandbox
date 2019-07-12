using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class SomewhatFancierExample2 : MonoBehaviour
{
	[RangeReactiveProperty(0, 100)]
	[SerializeField] private IntReactiveProperty _health;
	[RangeReactiveProperty(0, 150)]
	[SerializeField] private IntReactiveProperty _maxHealth;

	public ReactiveCommand Heal { get; private set; }
	public ReactiveCommand Hurt { get; private set; }
	public ReactiveCommand Revive { get; private set; }

	public IObservable<int> Health => _health;
	public IObservable<float> NormalizedHealth => _health.CombineLatest(_maxHealth, (h, m) => h / (float) m);

	private void Awake()
	{
		Hurt = new ReactiveCommand(_health.Select(h => h > 0));
		Revive = new ReactiveCommand(_health.Select(h => h <= 0));
		Heal = new ReactiveCommand(NormalizedHealth.Select(h => h < 1f));

		Heal.Subscribe(_ => _health.Value += Random.Range(10, 20)).AddTo(this);
		Hurt.Subscribe(_ => _health.Value -= Random.Range(5, 15)).AddTo(this);
		Revive.Subscribe(_ => _health.Value = _maxHealth.Value).AddTo(this);
	}
}
