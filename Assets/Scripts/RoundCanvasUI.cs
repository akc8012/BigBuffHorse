using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCanvasUI : MonoBehaviour
{
	[SerializeField] GameObject roundIndicator;
	[SerializeField] Text roundText;

	// ShowRoundWinner


	public void ShowRoundIndicator(int round)
	{
		roundText.text = "Round " + round + " Start!";
		roundIndicator.SetActive(true);
	}

	public void HideRoundIndicator()
	{
		roundIndicator.SetActive(false);
	}
}
