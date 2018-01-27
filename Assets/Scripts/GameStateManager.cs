﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Waiting, Playing }

public class GameStateManager : MonoBehaviour
{
	public static GameStateManager instance = null;

	[SerializeField]
	GameObject gameEndCanvas;

	GameState gameState = GameState.Waiting;
	public GameState GetState { get { return gameState; } }

	void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
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
		gameState = GameState.Playing;
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

	public void RoundEnd()
	{
		print(ScoreManager.instance.GetCurrentWinner());
		StartCoroutine(WaitForEndRound(3));

		gameState = GameState.Waiting;
	}

	IEnumerator WaitForEndRound(float t)
	{
		yield return new WaitForSeconds(t);
		gameState = GameState.Playing;

		SceneManager.LoadScene(1);
	}
}