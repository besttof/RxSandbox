using System;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

public class SomewhatFancierExample3 : MonoBehaviour
{
	public IObservable<ICoffee> OrderedCoffees { get; private set; }
	private ISubject<ICoffee> _manualOrders = new Subject<ICoffee>();
	
	private void Awake()
	{
		// merge a bunch of timers to create non-obvious order patterns.
		var intervals = Observable.Merge(
			Observable.Interval(TimeSpan.FromSeconds(3)),
			Observable.Interval(TimeSpan.FromSeconds(7)),
			Observable.Interval(TimeSpan.FromSeconds(17))
			);

		// Note that the Share part here is very important
		var randomCoffees = intervals.Select(_ => GetRandomCoffee()).Share();

		OrderedCoffees = Observable.Merge(randomCoffees, _manualOrders);
	}

	public void OrderEspresso()
	{
		_manualOrders.OnNext(new Espresso());
	}

	private ICoffee GetRandomCoffee()
	{
		var p = Random.Range(0, 10);

		if (p == 9) return new Latte();
		if (p > 6) return new Cappuccino();
		if (p > 3) return new Americano();
		return new Espresso();
	}
}
