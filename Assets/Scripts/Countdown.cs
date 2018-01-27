﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Countdown : MonoBehaviour
{
	[SerializeField]
	bool running = true;
	[SerializeField] int timeLeft;
	[SerializeField] Text text;

	void Awake()
	{
		StartCoroutine("IncrementCountdown");
	}

	IEnumerator IncrementCountdown()
	{
		while (running)
		{
			timeLeft--;
			SetTime(timeLeft);

			if (timeLeft <= 0)
			{
				GameStateManager.instance.GameEnd();
				running = false;
			}
			
			yield return new WaitForSeconds(1);
		}
	}

	public void SetTime(int value)
	{
		TimeSpan time = TimeSpan.FromSeconds(value);
		text.text = time.Minutes.ToString() + ":" + (time.Seconds < 10 ? "0" : "") + time.Seconds.ToString();
	}
}