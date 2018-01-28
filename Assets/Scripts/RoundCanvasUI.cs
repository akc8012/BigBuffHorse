using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCanvasUI : MonoBehaviour
{
	[SerializeField] GameObject roundIndicator;
	[SerializeField] Text roundText;

	public void ShowRoundIndicator(int round)
	{
		roundText.text = "Round " + round + " Start!";
		roundIndicator.SetActive(true);
	}

	public void ShowRoundWinner(string winner)
	{
		if (winner == "Tie")
			roundText.text = "It's a tie";
		else
			roundText.text = "Player " + winner + " Wins!";

		roundIndicator.SetActive(true);
	}

	public void HideRoundIndicator()
	{
		roundIndicator.SetActive(false);
	}
}
