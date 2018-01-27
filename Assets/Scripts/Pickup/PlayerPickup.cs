using UnityEngine;

public class PlayerPickup : MonoBehaviour {

	Interactable heldItem;
	Interactable seenItem;

	PlayerBallController controller;

	void Awake()
	{
		controller = GetComponent<PlayerBallController>();
	}

	void Update() {
		if (Input.GetButtonDown("Action" + controller.GetPlayerNdx))
		{
			if (seenItem)
			{
				seenItem.OnPickup(transform);
			}
		}
	}

	public void OnPickupStay(Collider pickup) {
		
	}

	public void OnPickupEnter(Collider pickup) {
		seenItem = pickup.GetComponent<Interactable>();
	}
}
