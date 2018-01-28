﻿using System.Collections;
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

		StartCoroutine(ShowRoundIndicator(3));
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
		print("GAME WINNER: " + ScoreManager.instance.GetGameWinner());

		Instantiate(gameEndCanvas);
		gameState = GameState.Waiting;
	}

	public void RoundStart()
	{
		GameObject.Find("Countdown").GetComponent<Countdown>().StartTimer();
		gameState = GameState.Playing;
	}

	public void RoundEnd()
	{
		RoundCanvasUI roundCanvasUI = GameObject.Find("RoundCanvas").GetComponent<RoundCanvasUI>();
		roundCanvasUI.ShowRoundWinner(ScoreManager.instance.GetCurrentWinnerString());
		
		StartCoroutine(WaitForEndRound(3));

		gameState = GameState.Waiting;
	}

	IEnumerator WaitForEndRound(float t)
	{
		yield return new WaitForSeconds(t);
		
		SceneManager.LoadScene(1);

		StartCoroutine(ShowRoundIndicator(t));
	}
}