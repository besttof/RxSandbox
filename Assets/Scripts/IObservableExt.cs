using System;
using UniRx;
using UnityEngine;

public static class IObservableExt
{
	public static IDisposable SubscribeAndLog<T>(this IObservable<T> observable, string tag = "")
	{
		return observable.Subscribe(
			x => Debug.Log($"<i>{tag}</i> {x}"),
			ex => Debug.Log($"<i>{tag}</i> OnError {ex}"),
			() => Debug.Log($"<i>{tag}</i> Completed"));
	}
}