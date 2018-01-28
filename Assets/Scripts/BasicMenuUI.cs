﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicMenuUI : MonoBehaviour
{
	[SerializeField] Button playAgainButt;
	[SerializeField] Button quitButt;

	void Start()
	{
		if (playAgainButt != null)
			playAgainButt.onClick.AddListener(RestartGameClicked);

		if (quitButt != null)
			quitButt.onClick.AddListener(QuitClicked);
	}

	void RestartGameClicked()
	{
		GameStateManager.instance.RestartGame();
	}

	void QuitClicked()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
			Application.Quit();
		else
			GameStateManager.instance.GoToTitle();
	}

}