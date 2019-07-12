using DG.Tweening;
using TMPro;
using UnityEngine;

public class PointsFloaterPresenter : MonoBehaviour
{
	[SerializeField] private TMP_Text _points;

	public void Init(int points)
	{
		_points.text = $"{points:+#}";
		transform.localEulerAngles = new Vector3(0, 0, Random.Range(-5f, 5f));

		transform.DOLocalMoveY(1, 2f)
		         .OnComplete(() => Destroy(gameObject))
		         .SetEase(Ease.OutCirc)
		         .SetRelative()
		         .SetId(this);
	}
}