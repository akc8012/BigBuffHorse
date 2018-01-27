﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
	[SerializeField] GameObject gameStateManager;
	[SerializeField] GameObject scoreManager;

	void Awake()
	{
		if (GameStateManager.instance == null)
			Instantiate(gameStateManager);

		if (ScoreManager.instance == null)
			Instantiate(scoreManager);
	}
}
