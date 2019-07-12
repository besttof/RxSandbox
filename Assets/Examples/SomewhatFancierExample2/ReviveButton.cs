using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ReviveButton : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample2 _source;
	[SerializeField] private Button _button;

	private void Start()
	{
		_source.Revive
			.BindTo(_button)
			.AddTo(this);
	}
}