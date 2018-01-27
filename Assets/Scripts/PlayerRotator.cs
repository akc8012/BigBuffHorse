using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
	[SerializeField] PlayerBallController playerBallController;

	Quaternion lastValidRot;
	
	void Start()
	{
		
	}

	void LateUpdate()
	{
		Vector3 dist = playerBallController.Distance.normalized; dist.y = 0;

		if (dist != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(dist);
			lastValidRot = transform.rotation;
		}
		else transform.rotation = lastValidRot;
	}
}
