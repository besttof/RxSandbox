using UniRx;
using UnityEngine;

public sealed class BallSpawner : MonoBehaviour
{
	[SerializeField] private GameState _gameState;
	[SerializeField] private Ball _ballPrefab;
	[SerializeField] private Transform _spout;
	[SerializeField] private float _spoutRotateSpeed;
	[SerializeField] private Vector2 _spawnOffset = new Vector2(0, -1);
	
	private void Start()
	{
		
		Observable.EveryUpdate()
		          .Where(_ => Input.GetKeyUp(KeyCode.Space))
		          .Subscribe(_ => _gameState.LaunchNewBall.Execute())
		          .AddTo(this);

		_gameState.LaunchNewBall.Subscribe(_ => SpawnBall())
		          .AddTo(this);
		
		_gameState.ActiveBalls
		          .ObserveRemove()
		          .Subscribe(x => Destroy(x.Value.gameObject))
		          .AddTo(this);

		_gameState.ActiveBalls
		          .ObserveAdd()
		          .Subscribe(x => _gameState.BallsLeft.Value--)
		          .AddTo(this);

		Observable.EveryFixedUpdate()
		          .Select(t => Mathf.Lerp(-45, 45, Mathf.PingPong(t * _spoutRotateSpeed, 1f)))
		          .Select(a => Quaternion.Euler(0, 0, a))
		          .Subscribe(a => _spout.rotation = a);
	}

	private void SpawnBall()
	{
		var ball = Instantiate(_ballPrefab, _spout.TransformPoint(_spawnOffset), Quaternion.identity);
		ball.Init(_spout.TransformVector(Vector2.down));
	}
}