using TMPro;
using UniRx;
using UnityEngine;

public class BallsLeftPresenter : MonoBehaviour
{
	[SerializeField] private GameState _gameState;
	[SerializeField] private TMP_Text _ballsLeftText;
	[SerializeField] private TMP_Text _ballsActiveText;

	private void Start()
	{
		_gameState.BallsLeft
		          .Subscribe(l => _ballsLeftText.text = $"Balls Left: {l}")
		          .AddTo(this);
		
		_gameState.ActiveBalls.ObserveCountChanged(true)
		          .Subscribe(l => _ballsActiveText.text = $"In play: {l}")
		          .AddTo(this);
	}
}