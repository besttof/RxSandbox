using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

public class RecentOrdersPresenter : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample3 _source;
	[SerializeField] private int _orderhistory = 3;
	[SerializeField] private TMP_Text _text;

	private void Start()
	{
		_source.OrderedCoffees
			.Timestamp()
			.Select(s => $"{s.Timestamp:hh:mm}: {s.Value.GetType().Name}")
			.Buffer(_orderhistory, 1)
			.Select(l => string.Join(",\n", l.ToArray()))
			.Subscribe(orders => _text.text = $"Last {_orderhistory} orders:\n{orders}")
			.AddTo(this);
	}
}
