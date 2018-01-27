﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
	[SerializeField] int playerNdx = 0;
	[SerializeField] float speedModifier = 1.0f;

	Camera cam;
	Rigidbody rb;
	Vector3 lastPos;
	Vector3 distanceInFrame;

	public Vector3 Distance { get { return distanceInFrame; } }
	public int GetPlayerNdx { get { return playerNdx; } }

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		cam = Camera.main;
	}
	
	void FixedUpdate()
	{
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal" + playerNdx), 0, Input.GetAxisRaw("Vertical" + playerNdx));
		Vector3 movement = GetCameraRelativeMovement(cam.transform, input);

		rb.AddForce(Vector3.down, ForceMode.VelocityChange);
		rb.AddForce(movement * speedModifier, ForceMode.VelocityChange);


		Vector3 potentialDistance = transform.position - lastPos;
		if (potentialDistance.magnitude > 0.01f)
			distanceInFrame = potentialDistance;

		lastPos = transform.position;
	}

	Vector3 GetCameraRelativeMovement(Transform cam, Vector3 input)
	{
		float speed = Mathf.Clamp(Vector3.Magnitude(input), 0, 1);

		Vector3 cameraDir = cam.forward; cameraDir.y = 0.0f;
		Vector3 moveDir = Quaternion.FromToRotation(Vector3.forward, cameraDir) * input;

		return moveDir.normalized * speed;
	}
}
