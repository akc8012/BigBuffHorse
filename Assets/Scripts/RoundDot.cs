using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundDot : MonoBehaviour
{
	Image image;
	[SerializeField] int index;
	[SerializeField] Color P0;
	[SerializeField] Color P1;
	[SerializeField] Color Nothing;

	void Start()
	{
		image = GetComponent<Image>();
		SetDot();
	}

	void Update()
	{
		if (Time.frameCount % 5 == 0)
			SetDot();
	}

	void SetDot()
	{
		if (ScoreManager.instance.GetRoundWinners[index] == WinState.P0)
			image.color = P0;
		else if (ScoreManager.instance.GetRoundWinners[index] == WinState.P1)
			image.color = P1;
		else
			image.color = Nothing;
	}
}
