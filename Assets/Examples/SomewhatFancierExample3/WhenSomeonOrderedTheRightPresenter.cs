using UniRx;
using UnityEngine;

public class WhenSomeonOrderedTheRightPresenter : MonoBehaviour
{
	[SerializeField] private SomewhatFancierExample3 _source;
	[SerializeField] private ParticleSystem _system;

	private void Start()
	{
		_source.OrderedCoffees
			.OfType<ICoffee, Espresso>()
			.Subscribe(_ => _system.Play())
			.AddTo(this);
	}
}
