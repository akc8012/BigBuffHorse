using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
	[SerializeField] Text scoreText;

	int score = 0;
	public int GetScore { get { return score; } }

	void Awake()
	{
		UpdateDisplay();
	}

	public void AddScore(int points)
	{
		if (GameStateManager.instance.GetState == GameState.Waiting)
			return;

		score += points;

		SoundManager.instance.PlayClip("score" + Random.Range(0, 2));
		if (Random.Range(0, 100) < 10)
			SoundManager.instance.PlayClip("cheer" + Random.Range(0, 2));

		UpdateDisplay();
	}

	void UpdateDisplay()
	{
		scoreText.text = score.ToString("0000");
	}
}
