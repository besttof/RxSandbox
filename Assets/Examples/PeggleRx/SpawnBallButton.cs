using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public sealed class SpawnBallButton : MonoBehaviour
{
	[SerializeField] private GameState _gameState;
	[SerializeField] private Button _button;

	private void Start()
	{
		_gameState.LaunchNewBall
		          .BindTo(_button)
		          .AddTo(this);
	}
}