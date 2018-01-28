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
			if (!heldItem)
			{
				// PICKUP
				if (seenItem)
				{
					Debug.Log("Picking up " + seenItem.gameObject.name + "!");
					seenItem.OnPickup(transform);
					heldItem = seenItem;
				}
			}
			else
			{
				// DROP
				Debug.Log("Dropping " + seenItem.gameObject.name + ".");
				heldItem.OnDrop(transform);
				heldItem = null;
			}
		}
	}

	public void OnPickupStay(Collider pickup)
	{
		
	}

	public void OnPickupEnter(Collider pickup)
	{
		//Debug.Log("I see " + pickup.gameObject.name + "!");
		seenItem = pickup.GetComponent<Interactable>();
	}
}
