using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SomewhatFancierExample1 : MonoBehaviour
{
	[RangeReactiveProperty(0f, 1f)]
	[SerializeField] private FloatReactiveProperty _pumaProgress;

	[SerializeField] private Transform _start;
	[SerializeField] private Transform _end;
	
	public IObservable<float> PumaProgress => _pumaProgress;
	public IObservable<Vector3> PumaPosition => _pumaProgress.Select(p => Vector3.Lerp(_start.position, _end.position, p));
}
