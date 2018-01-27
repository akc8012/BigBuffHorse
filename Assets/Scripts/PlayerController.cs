using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	void Start()
	{
		
	}
	
	void Update()
	{
		if (Input.GetAxisRaw("Horizontal") >= 0.5f)
			print("memes");
	}
}
