using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
	[SerializeField] int playerNdx = 0;
	[SerializeField] Text scoreText;

	int score = 0;
	public int GetScore { get { return score; } }

	void Awake()
	{
		UpdateDisplay();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && playerNdx == 0)
			AddScore(10);

		else if (Input.GetKeyDown(KeyCode.P) && playerNdx == 1)
			AddScore(10);
	}

	public void AddScore(int points)
	{
		if (GameStateManager.instance.GetState == GameState.Waiting)
			return;

		score += points;
		UpdateDisplay();
	}

	void UpdateDisplay()
	{
		scoreText.text = score.ToString("0000");
	}
}
