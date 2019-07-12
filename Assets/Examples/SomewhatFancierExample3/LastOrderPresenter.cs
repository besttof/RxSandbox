using TMPro;
using UniRx;
using UnityEngine;

public class LastOrderPresenter : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample3 _source;
	[SerializeField] private TMP_Text _text;

	private void Start()
	{
		_source.OrderedCoffees
			.Subscribe(order => _text.text = $"Last order:\n{order}")
			.AddTo(this);
	}
}