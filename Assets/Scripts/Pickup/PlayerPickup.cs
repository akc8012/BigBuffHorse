using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
	Interactable heldItem;
	Interactable seenItem;
	PlayerBallController controller;
	[SerializeField] Animator animator;
	[SerializeField] IKControl ik;
	[SerializeField] Transform throwTransform;
	[SerializeField] float throwForce = 10;

	void Awake()
	{
		controller = GetComponentInChildren<PlayerBallController>();
	}

	void Update()
	{
		if (GameStateManager.instance.GetState == GameState.Playing)
		{
			if (Input.GetButtonDown("Action" + controller.GetPlayerNdx))
			{
				if (!heldItem)
				{
					// PICKUP
					if (seenItem)
					{
						seenItem.OnPickup(transform);
						heldItem = seenItem;
					}
				}
				else
				{
					// DROP
					heldItem.OnDrop(transform);
					heldItem = null;
				}
			}

			if (Input.GetButtonDown("Throw" + controller.GetPlayerNdx))
			{
				animator.SetTrigger("Throw");
				Throw();
			}
		}
	}

	public void OnPickupStay(Collider pickup)
	{
		
	}

	public void OnPickupEnter(Collider pickup)
	{
		seenItem = pickup.GetComponent<Interactable>();
	}

	public void OnPickupExit(Collider pickup)
	{
		seenItem = null;
	}

	public void Throw()
	{
		if (heldItem)
		{
			ik.ikActive = false;
			heldItem.GetComponent<Interactable>().OnDrop(transform);
			heldItem.transform.position = throwTransform.position;
			heldItem.GetComponent<Rigidbody>().AddForce(throwTransform.forward * throwForce, ForceMode.Impulse);	
			heldItem = null;
		}
	}
}
