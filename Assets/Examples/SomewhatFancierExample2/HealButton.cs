using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HealButton : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample2 _source;
	[SerializeField] private Button _button;

	private void Start()
	{
		_source.Heal
			.BindTo(_button)
			.AddTo(this);
	}
}
