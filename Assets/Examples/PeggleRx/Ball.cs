using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private GameState _gameState;
	[SerializeField] private Rigidbody2D _rigidbody2D;
	[SerializeField] private float _directionScale;

	[SerializeField] private AudioSource _audio;
	
	private void Start()
	{
		_gameState.ActiveBalls.Add(this);
		
		this.OnCollisionEnter2DAsObservable()
		    .ThrottleFirstFrame(2)
		    .Subscribe(_ => PlayHitEffect())
		    .AddTo(this);
	}

	private void PlayHitEffect()
	{
		_audio.Play();
		_audio.pitch += 0.01f;
	}

	private void OnDestroy()
	{
		_gameState.ActiveBalls.Remove(this);
	}
	
	public void Init(Vector2 direction)
	{
		_rigidbody2D.velocity = direction * _directionScale;
	}
}