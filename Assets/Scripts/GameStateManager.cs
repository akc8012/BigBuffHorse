﻿﻿using System.Collections;
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
	}

	public void StartGame()
	{
		SceneManager.LoadScene(1);
		gameState = GameState.Playing;
	}

	public void GoToTitle()
	{
		SceneManager.LoadScene(0);
	}

	public void GameEnd()
	{
		print(ScoreManager.instance.GetCurrentWinner());

		Instantiate(gameEndCanvas);
		gameState = GameState.Waiting;
	}
}