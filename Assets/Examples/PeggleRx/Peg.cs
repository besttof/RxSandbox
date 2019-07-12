using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

public sealed class Peg : MonoBehaviour
{
	[SerializeField] private GameState _gameState;

	[SerializeField] private int _points;
	[SerializeField] private int _hits;
	
	private readonly ReactiveProperty<int> _health = new ReactiveProperty<int>();

	private void Start()
	{
		_health.Value = _hits;
		_health.Where(h => h <= 0)
		       .Subscribe(_ => Remove())
		       .AddTo(this);
		/**/
		_health.Where(h => h >= 0)
		       .Skip(1)
		       .Subscribe(h => PlayHitEffects(h))
		       .AddTo(this);
		/** /
		_health.Pairwise()
		       .Where(p => p.Current < p.Previous)
		       .Select(p => p.Current)
		       .Subscribe(h => PlayHitEffects(h))
		       .AddTo(this);
		/**/
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		_health.Value--;
	}

	private void Remove()
	{
		_gameState.ScorePoints(gameObject, _points);
		Destroy(gameObject);
	}

	private void PlayHitEffects(int i)
	{
//		var scale = (i + 1) * 0.25f;
		var scale = 0.25f;
		DOTween.Complete(this);
		transform.DOPunchScale(new Vector3(scale, scale), 1f)
		         .SetId(this);
	}
}