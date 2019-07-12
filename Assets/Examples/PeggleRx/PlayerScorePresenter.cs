using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

public class PlayerScorePresenter : MonoBehaviour
{
	[SerializeField] private GameState _gameState;
	[SerializeField] private TMP_Text _scoreText;
	[SerializeField] private TMP_Text _multiplierText;

	[SerializeField] private AudioSource _scoreAudio;

	private void Start()
	{
		_gameState
			.ScoredPoints
			.ThrottleFirstFrame(2)
			.Subscribe(_ => PlayScoreEffect())
			.AddTo(this);

		_gameState
			.Score
			.Throttle(TimeSpan.FromMilliseconds(300))
			.StartWith(0)
			.Pairwise()
			.Subscribe(x => TweenScoreText(x.Previous, x.Current))
			.AddTo(this);

		_gameState
			.Multiplier
			.Subscribe(x => TweenMultiplierText(x))
			.AddTo(this);
	}

	private void PlayScoreEffect()
	{
		_scoreAudio.Play();

		DOTween.Complete(_scoreText);
		_scoreText.transform
		          .DOPunchScale(new Vector3(0.2f, 0.1f), 2f)
		          .SetId(_scoreText);
	}

	private void TweenMultiplierText(int multiplier)
	{
		DOTween.Complete(_multiplierText);
		_multiplierText.text = multiplier > 1 ? $"x{multiplier}" : "";

		_multiplierText.transform
		               .DOPunchScale(new Vector3(0.2f, 0.1f), 2f)
		               .SetId(_multiplierText);
	}

	private void TweenScoreText(int from, int to)
	{
		DOTween.Complete(this);
		DOVirtual.Float(from, to, 1.5f, s => _scoreText.text = $"Score: {s:0}")
		         .SetEase(Ease.InOutCirc)
		         .SetId(this);
	}
}