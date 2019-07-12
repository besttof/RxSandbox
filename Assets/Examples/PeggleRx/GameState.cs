using System;
using UniRx;
using UnityEngine;

[CreateAssetMenu]
public sealed class GameState : ScriptableObject
{
	[SerializeField] private int _startBalls;
	[SerializeField] private int _maxActiveBalls = 1;

	public ReactiveCommand LaunchNewBall { get; private set; }
	public IObservable<int> ScoredPoints => _scoredPoints.Select(x => x.Points).AsObservable();
	public IObservable<PointsData> ScoredPointsData => _scoredPoints.AsObservable();

	public readonly ReactiveCollection<Ball> ActiveBalls = new ReactiveCollection<Ball>();

	public readonly ReactiveProperty<int> Score = new ReactiveProperty<int>();
	public readonly ReactiveProperty<int> Multiplier = new ReactiveProperty<int>(1);
	public readonly ReactiveProperty<int> BallsLeft = new ReactiveProperty<int>();
	public readonly ReactiveProperty<int> MaxActiveBalls = new ReactiveProperty<int>();

	private readonly ISubject<PointsData> _scoredPoints = new Subject<PointsData>();

	private void OnEnable()
	{
		BallsLeft.Value = _startBalls;
		MaxActiveBalls.Value = _maxActiveBalls;

		var enoughBallsLeft = BallsLeft.Select(b => b > 0);
		var balls = Observable.CombineLatest(ActiveBalls.ObserveCountChanged(true), 
		                                     MaxActiveBalls, 
		                                     (active, max) => active < max);

		var canSpawn = Observable.CombineLatest(enoughBallsLeft,
		                                        balls,
		                                        (ballsLeft, noneActive) => ballsLeft && noneActive);
		
		LaunchNewBall = new ReactiveCommand(canSpawn);
	}

	public void ScorePoints(GameObject source, int points)
	{
		_scoredPoints.OnNext(new PointsData(points * Multiplier.Value, source));
		Score.Value += points; // we could also do: _scoredPoints.Subscribe(p => Score.Value += p.Points)
	}
}