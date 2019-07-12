using System;
using UniRx;
using UnityEngine;

public sealed class ComboController : MonoBehaviour
{
	[SerializeField] private int _comboCount = 3;
	
	[SerializeField] private GameState _gameState;
	[SerializeField] private PointsFloaterPresenter _pointsFloater;

	private void Start()
	{
		var interval = TimeSpan.FromMilliseconds(500);
		var rapidScore = _gameState.Score.Throttle(interval);
		
		_gameState.ScoredPointsData
		          .ThrottleFirst(interval)
		          .Buffer(rapidScore)
		          .Where(b => b.Count >= _comboCount)
		          .Subscribe(_ => _gameState.Multiplier.Value++)
		          .AddTo(this);

		_gameState.Multiplier
		          .Throttle(TimeSpan.FromSeconds(5))
		          .Subscribe(_ => _gameState.Multiplier.Value = 1)
		          .AddTo(this);
	}
}