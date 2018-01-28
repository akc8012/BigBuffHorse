﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Countdown : MonoBehaviour
{
	[SerializeField]
	bool running = true;
	[SerializeField] int startTime = 120;
	int timeLeft;
	[SerializeField] Text text;

	void Start()
	{
		timeLeft = startTime;
	}

	public void StartTimer()
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
				EndCountdown();

			SoundManager.instance.PlayClip("tick" + UnityEngine.Random.Range(0, 5));
			yield return new WaitForSeconds(1);
		}
	}

	public void Reset()
	{
		SetTime(startTime);
	}

	public void SetTime(int value)
	{
		TimeSpan time = TimeSpan.FromSeconds(value);
		text.text = time.Minutes.ToString() + ":" + (time.Seconds < 10 ? "0" : "") + time.Seconds.ToString();
	}

	void EndCountdown()
	{
		GameObject.Find("WhistleControl").GetComponent<Whistle>().Play();
		running = false;
	}
}
