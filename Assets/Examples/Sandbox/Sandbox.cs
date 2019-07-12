using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Sandbox : MonoBehaviour
{
	[SerializeField] private Button _button;
	[SerializeField] private IntReactiveProperty _intProperty;

	private void Start()
	{
		// You can follow the lines below and uncomment each part individually to see what it does.

		// These are two observable streams, not that as long as nothing subscribes
		// to them, they don't start emitting values (i.e. they're cold observables)
		var interval = Observable.Interval(TimeSpan.FromSeconds(1));
		var buttonClick = _button.OnClickAsObservable();

		// The most basic way to subscribe to a stream, only responding to OnNext
		// Note that the value the interval emits is a long indicating the amount
		// of ticks
		//interval.Subscribe(x => Debug.Log(x));

		// The complete wat to subscribe to all events from an observable (OnNext, OnError and OnComplete)
		//interval.Subscribe(x => Debug.Log(x),
		//	ex => Debug.LogError($"Error {ex}"),
		//	() => Debug.Log("Complete"));

		// For this sandbox I've created an overload to Subscribe and log each event
		// For brevity's sake I'll use that from here on.
		// These subscribe to the stream using that overload (delete or comment the subscrioptions
		// above)
		//interval.SubscribeAndLog("Interval");
		//buttonClick.SubscribeAndLog("Button");
		//_intProperty.SubscribeAndLog("Property");
			   
		// Timestamp decorates events with the time they are emitted, this can be very useful
		// (I use it in events below). Again, note that they don't emit anything until something
		// subscribes/
		//var intervalTimed = interval.Timestamp();
		//var buttonClickTimed = buttonClick.Timestamp();

		// Select, projects the value to something else. In this case another number, but you're
		// free to change the type completely
		//interval.Select(i => i * 3).SubscribeAndLog("Select");

		// Where, filters the source stream and only emits values that meet the predicate criteria
		//interval.Where(i => i % 3 == 0).SubscribeAndLog("Where");

		// Take/Skip, only take a fixed number of values/skip a fixed number of values. Note that
		// the Take will complete the stream after the given number of emitted values.
		//interval.Take(3).SubscribeAndLog("Take");
		//interval.Skip(3).SubscribeAndLog("Skip");

		// Delay, well, delays every emitted value
		//buttonClick.Delay(TimeSpan.FromSeconds(2)).SubscribeAndLog("Delay");

		// TakeUntil, takes values from the source stream until another stream emits a value. Also
		// completes the stream like regular "Take". UniRx also has MonoBehaviour variants that allow
		// you to take until disable or destroy.
		//interval.TakeUntil(buttonClick).SubscribeAndLog("TakeUntil");
		//interval.TakeUntilDisable(this).SubscribeAndLog("TakeUntilDisable");

		// SkipUntil, don't start emitting values from the source until another stream has emitted a
		// value.
		//interval.SkipUntil(buttonClick).SubscribeAndLog("SkipUntil");

		// Throttle, does not emit a value as long as the source value keep following each other within
		// the given timeframe. When no value is emitted for the timeframe, it will emit the most
		// recent one. This is useful if you have something that can change a lot and you're only
		// interested in a recent value, not every value per se. For example if a long running process
		// is kicked off by it.
		//buttonClickTimed.Throttle(TimeSpan.FromMilliseconds(500)).SubscribeAndLog("Throttle");

		// PairWise, pairs all values. Not that this won't emit anuthing until at lease two values
		// have been emitted by the source
		//interval.Pairwise().Select(x => $"Previous {x.Previous} Current {x.Current}").SubscribeAndLog("PairWise");

		// Merge, merges all streams (can be more than just two) into a single stream. Note that
		// for this the type of the emitted values has to match. Select is very useful in these cases
		//interval.Merge(buttonClick.Select(_ => 9001L)).Select(x => $"♫ {x}").SubscribeAndLog("Merge");

		// CombineLatest, combines the latest values of different source streams. I used
		// the timestamped streams here to show the original emitted times. Note that this will emit
		// whenever any of the input streams emit a new value.
		//intervalTimed.CombineLatest(buttonClickTimed, (i, b) => $"♫ {i.Timestamp:T}, ⬇ {b.Timestamp:T}").SubscribeAndLog("CombineLatest");

		// Zip, combines pairs of emitted items but keeps the sequence in tact. So the first items of
		// all source streams are combined, the seconds items are combined, etc. This is a tricky
		// operator when the fequency of emitted items varies too much between the source streams.
		//intervalTimed.Zip(buttonClickTimed, (i, b) => $"♫ {i.Timestamp:T}, ⬇ {b.Timestamp:T}").SubscribeAndLog("Zip");

		// ZipLatest, like zip only emits when all source streams emit a new item, but instead of
		// pairing the first with the first items, it pairs the latest. So, it uses the combination
		// logic of CombineLatest, but the emission conditions of Zip
		//intervalTimed.ZipLatest(buttonClickTimed, (i, b) => $"♫ {i.Timestamp:T}, ⬇ {b.Timestamp:T}").SubscribeAndLog("ZipLatest");

		// TimeInterval, decorates the emitted values like timestamp but uses the time between
		// emitted values instead of the emission time.
		//buttonClick.TimeInterval().SubscribeAndLog("TimeInterval");
	}
}