using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(other.gameObject, 0.1f);
	}
}