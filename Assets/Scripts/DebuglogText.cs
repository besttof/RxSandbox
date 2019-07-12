using System.Text;
using TMPro;
using UniRx;
using UnityEngine;

public class DebuglogText : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;

	private StringBuilder _buffer = new StringBuilder();

	private void OnEnable()
	{
		_buffer.Clear();

		Observable.FromEvent<Application.LogCallback, string>(
			          h => (condition, stackTrace, logType) => h(condition),
			          h => Application.logMessageReceived += h,
			          h => Application.logMessageReceived -= h)
		          .TakeUntilDisable(this)
		          .Timestamp()
		          .Subscribe(
			          x =>
			          {
				          _buffer.AppendLine($"<size=60%>[{x.Timestamp:hh:mm:ss.fff}]</size> {x.Value}");
				          _text.text = _buffer.ToString();
			          });
	}
}