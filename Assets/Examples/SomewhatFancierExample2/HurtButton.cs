using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HurtButton : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample2 _source;
	[SerializeField] private Button _button;

	private void Start()
	{
		_source.Hurt
			.BindTo(_button)
			.AddTo(this);
	}
}
