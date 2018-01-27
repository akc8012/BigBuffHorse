using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WinState { P0, P1, Tie }

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance = null;

	ScoreDisplay[] scoreDisplays;

	int round = 0;

	void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		SceneManager.sceneLoaded += OnSceneLoad;
	}

	void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
	{
		scoreDisplays = new ScoreDisplay[2];
		scoreDisplays[0] = GameObject.Find("ScoreDisplay P0").GetComponent<ScoreDisplay>();
		scoreDisplays[1] = GameObject.Find("ScoreDisplay P1").GetComponent<ScoreDisplay>();
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
}
