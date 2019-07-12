using UniRx;
using UnityEngine;

public class ProgressBackgroundColor : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample1 _source;
	[SerializeField] private Gradient _gradient;
	[SerializeField] private Camera _camera;

	private void Start()
	{
		_source.PumaProgress
			.Subscribe(s => _camera.backgroundColor= _gradient.Evaluate(s))
			.AddTo(this);
	}
}