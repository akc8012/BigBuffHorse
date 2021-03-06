﻿using UnityEngine;

public class PlayerFallDown : MonoBehaviour
{
	[SerializeField] GameObject RagdollPrefab;
	[SerializeField] Rigidbody playerRigidBody;
	[SerializeField] Collider playerCollider;
 	[SerializeField] SkinnedMeshRenderer playerRenderer;
	[SerializeField] PlayerBallController ballController;

	GameObject ragDoll;

	public void FallDown()
	{
		if (!ballController.fallen)
		{
			ballController.fallen = true;
			playerRenderer.enabled = false;
			playerCollider.enabled = false;
			playerRigidBody.isKinematic = true;
			ragDoll = Instantiate(RagdollPrefab, transform.position + Vector3.down * .5f, transform.rotation);
			Invoke("GetUp", 3);
		}
	}

	public void GetUp()
	{
		Destroy(ragDoll);
		playerRenderer.enabled = true;
		playerCollider.enabled = true;
		playerRigidBody.isKinematic = false;
		ballController.fallen = false;
	}
}
