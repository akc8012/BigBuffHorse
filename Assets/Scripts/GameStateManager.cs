﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Waiting, Playing }

public class GameStateManager : MonoBehaviour
{
	public static GameStateManager instance = null;

	[SerializeField] GameObject gameEndCanvas;
	[SerializeField] bool startImmediately = false;

	[SerializeField] GameState gameState = GameState.Waiting;
	public GameState GetState { get { return gameState; } }

	public delegate void RoundAction();
	public event RoundAction OnRoundStart;

	public delegate void RoundActionEnd();
	public event RoundActionEnd OnRoundEnd;

	void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		if (startImmediately)
			SetPlayingWhenStartingInGame();
	}

	void SetPlayingWhenStartingInGame()
	{
		if (SceneManager.GetActiveScene().buildIndex != 0)
			gameState = GameState.Playing;
	}

	public void RestartGame()
	{
		ScoreManager.instance.ResetScore();
		SceneManager.LoadScene(1);

		StartCoroutine(ShowRoundIndicator(2.15f));
	}

	IEnumerator ShowRoundIndicator(float t)
	{
		yield return null;
		RoundCanvasUI roundCanvasUI = GameObject.Find("RoundCanvas").GetComponent<RoundCanvasUI>();
		roundCanvasUI.ShowRoundIndicator(ScoreManager.instance.GetRound() + 1);

		yield return new WaitForSeconds(t);

		RoundStart();
		roundCanvasUI.HideRoundIndicator();
	}

	public void GoToTitle()
	{
		SceneManager.LoadScene(0);
	}

	public void GameEnd()
	{
		GameObject canvas = Instantiate(gameEndCanvas);
		canvas.GetComponent<RoundCanvasUI>().ShowRoundWinner(ScoreManager.instance.GetWinnerString(ScoreManager.instance.GetGameWinner()));

		gameState = GameState.Waiting;

		if (OnRoundEnd != null)
			OnRoundEnd();
	}

	public void RoundStart()
	{
		GameObject.Find("Countdown").GetComponent<Countdown>().StartTimer();
		gameState = GameState.Playing;

		if (OnRoundStart != null)
			OnRoundStart();
	}

	public void RoundEnd()
	{
		RoundCanvasUI roundCanvasUI = GameObject.Find("RoundCanvas").GetComponent<RoundCanvasUI>();
		roundCanvasUI.ShowRoundWinner(ScoreManager.instance.GetWinnerString(ScoreManager.instance.GetCurrentWinner()));
		
		StartCoroutine(WaitForEndRound(2.15f));

		gameState = GameState.Waiting;

		if (OnRoundEnd != null)
			OnRoundEnd();
	}

	IEnumerator WaitForEndRound(float t)
	{
		yield return new WaitForSeconds(t);
		
		SceneManager.LoadScene(1);

		StartCoroutine(ShowRoundIndicator(t));
	}
}