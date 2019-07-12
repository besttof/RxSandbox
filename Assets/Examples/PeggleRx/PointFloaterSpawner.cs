using UniRx;
using UnityEngine;

public sealed class PointFloaterSpawner : MonoBehaviour
{
	[SerializeField] private GameState _gameState;
	[SerializeField] private PointsFloaterPresenter _pointsFloater;

	private void OnEnable()
	{
		_gameState.ScoredPointsData
		          .TakeUntilDisable(this)
		          .Subscribe(p => SpawnFloater(p.Points, p.Source))
		          .AddTo(this);
	}

	private void SpawnFloater(int points, GameObject source)
	{
		var floater = Instantiate(_pointsFloater);
		floater.Init(points);
		floater.transform.position = source.transform.position;
	}
}