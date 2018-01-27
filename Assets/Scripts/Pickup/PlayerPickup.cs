using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
	Interactable heldItem;
	Interactable seenItem;
	PlayerBallController controller;

	void Awake()
	{
		controller = GetComponentInChildren<PlayerBallController>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Action" + controller.GetPlayerNdx))
		{
			Debug.Log("Pressing pickup button!");
			if (seenItem != null)
			{
				Debug.Log("Picking up " + seenItem.gameObject.name + "!");
				seenItem.OnPickup(transform);
			}
		}
	}

	public void OnPickupStay(Collider pickup)
	{
		
	}

	public void OnPickupEnter(Collider pickup)
	{
		Debug.Log("I see " + pickup.gameObject.name + "!");
		seenItem = pickup.GetComponent<Interactable>();
	}
}
