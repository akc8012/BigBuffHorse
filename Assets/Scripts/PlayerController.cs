using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	CharacterController characterController;
	float rotSmooth = 45;
	float speed = 0.15f;

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	void Update()
	{
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		if (Vector3.Angle(input, transform.forward) > 135)
			transform.forward = input;
		else
		{
			Vector3 targetRotation = Vector3.Lerp(transform.forward, input, Time.deltaTime * rotSmooth);
			if (targetRotation != Vector3.zero)
				transform.rotation = Quaternion.LookRotation(targetRotation);
		}

		characterController.Move(transform.forward * input.magnitude * speed);
	}
}
