using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WinState { P0, P1, Tie, Empty }

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance = null;

	ScoreDisplay[] scoreDisplays;

	[SerializeField] WinState[] roundWinners;
	public WinState[] GetRoundWinners { get { return roundWinners; } } 

	void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		SceneManager.sceneLoaded += OnSceneLoad;

		ResetScore();
	}

	public void ResetScore()
	{
		roundWinners = new WinState[] { WinState.Empty, WinState.Empty, WinState.Empty };
	}

	void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
	{
		scoreDisplays = new ScoreDisplay[2];

		if (GameObject.Find("ScoreDisplay P0") && GameObject.Find("ScoreDisplay P1"))
		{
			scoreDisplays[0] = GameObject.Find("ScoreDisplay P0").GetComponent<ScoreDisplay>();
			scoreDisplays[1] = GameObject.Find("ScoreDisplay P1").GetComponent<ScoreDisplay>();
		}
	}

	public int GetRound()
	{
		for (int i = 0; i < roundWinners.Length; i++)
		{
			if (roundWinners[i] == WinState.Empty)
				return i;
		}

		return roundWinners.Length;
	}

	public WinState GetCurrentWinner()
	{
		if (scoreDisplays[0].GetScore == scoreDisplays[1].GetScore)
			return WinState.Tie;
		else if (scoreDisplays[0].GetScore > scoreDisplays[1].GetScore)
			return WinState.P0;
		else
			return WinState.P1;
	}

	public WinState GetGameWinner()
	{
		int p0Wins = 0, p1Wins = 0;

		for (int i = 0; i < roundWinners.Length; i++)
		{
			if (roundWinners[i] == WinState.P0)
				p0Wins++;
			if (roundWinners[i] == WinState.P1)
				p1Wins++;
		}

		if (p0Wins == p1Wins)
			return WinState.Tie;
		else if (p0Wins > p1Wins)
			return WinState.P0;
		else
			return WinState.P1;
	}

	public void RoundEnd()
	{
		SetWinner(GetCurrentWinner());

		if (GetRound() < 3)
			GameStateManager.instance.RoundEnd();
		else
			GameStateManager.instance.GameEnd();
	}

	void SetWinner(WinState winner)
	{
		roundWinners[GetRound()] = winner;
	}
}
