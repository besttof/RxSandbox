using UniRx;
using UnityEngine;

public class PumaPresenter : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample1 _source;

	private void Start()
	{
		_source.PumaPosition
			.Subscribe(pos => transform.position = pos)
			.AddTo(this);

		_source.PumaProgress
			.Subscribe(p => transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(p * 32) * 20))
			.AddTo(this);
	}
}