﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicMenuUI : MonoBehaviour
{
	[SerializeField]
	Button playAgainButt;
	[SerializeField]
	Button quitButt;

	void Start()
	{
		if (playAgainButt != null)
			playAgainButt.onClick.AddListener(RestartGameClicked);

		if (quitButt != null)
			quitButt.onClick.AddListener(GoToTitleClicked);
	}

	void RestartGameClicked()
	{
		GameStateManager.instance.StartGame();
	}

	void GoToTitleClicked()
	{
		GameStateManager.instance.GoToTitle();
	}

}