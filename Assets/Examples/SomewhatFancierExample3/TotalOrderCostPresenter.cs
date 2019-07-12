using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

public class TotalOrderCostPresenter : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample3 _source;
	[SerializeField] private TMP_Text _text;

	private void Start()
	{
		_source.OrderedCoffees
			.Select(c => c.Price)
			.Scan(0f, (a, v) => a + v)
			.Subscribe(total => _text.text = $"Total: {total:C}")
			.AddTo(this);
	}
}
