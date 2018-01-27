using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
	[SerializeField]
	bool running = true;
	[SerializeField] int timeLeft;
	[SerializeField] Text text;
	[SerializeField] UnityEvent onTimeEnd;

	void Awake()
	{
		StartCoroutine("IncrementCountdown");
	}

	IEnumerator IncrementCountdown()
	{
		while (running)
		{
			timeLeft--;
			text.text = timeLeft.ToString();

			if (timeLeft <= 0)
			{
				onTimeEnd.Invoke();
				running = false;
			}

			yield return new WaitForSeconds(1);
		}
	}

	public void SetTime(int value)
	{
		timeLeft = value;
		text.text = timeLeft.ToString();
	}
}
