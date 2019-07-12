using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class SubjectSandbox : MonoBehaviour
{
	private static uint _id;

	// Subjects are really useful, but it is recommended to never publicly expose a subject.
	// They're also a great example of cold/hot observables. For example: try emitting some
	// values and then create new subscriptions and then emit more values.

	// This is also a good time to invenstigate what happens when you subsribe to something
	// that is already completed, or errored. Or what happends if you try OnNext on something
	// that is completed or errored.

	// They come in different flavors, try each one by keeping a single one uncommented:

	// Standard subjects don't do anything special with the emitted values
	private readonly ISubject<int> _subject = new Subject<int>();

	// BehaviorSubjects always emit the latest values to new subscribers, ensuring there
	// is always a value available. That is why the constructor needs an initial value.
	//private readonly ISubject<int> _subject = new BehaviorSubject<int>(42);

		// AsyncSubjects (I don't get the name) only emit the latest value when the subject
	// completes.
	//private readonly ISubject<int> _subject = new AsyncSubject<int>();

	// ReplaySubjects replay x number of items to new subsribers
	//private readonly ISubject<int> _subject = new ReplaySubject<int>(3);

	private void Start()
	{
		_subject.SubscribeAndLog("Subject").AddTo(this);
	}

	private void OnGUI()
	{
		if (GUILayout.Button("OnNext"))
		{
			_subject.OnNext(Random.Range(1, 100));
		}

		if (GUILayout.Button("OnCompleted"))
		{
			_subject.OnCompleted();
		}

		if (GUILayout.Button("OnError"))
		{
			_subject.OnError(new Exception("I Am Error."));
		}

		if (GUILayout.Button("New Subscription"))
		{
			var id = _id++;
			Debug.Log($"New subject subscription {id}");

			_subject.SubscribeAndLog($"Subject {id}").AddTo(this);
		}
	}
}
