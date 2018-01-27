﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
	[SerializeField]
	GameObject gameStateManager;

	void Awake()
	{
		if (GameStateManager.instance == null)
			Instantiate(gameStateManager);
	}
}