using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBox : MonoBehaviour
{
	Rigidbody rb;
	Collider col;
	PlayerHolder[] players;
	[SerializeField] float pickupRadius = 2;

	void Start()
	{
		col = GetComponent<Collider>();
		rb = GetComponent<Rigidbody>();

		GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
		players = new PlayerHolder[playerObjs.Length];

		if (players.Length > 2)
			Debug.LogWarning("There shouldn't be more than 2 objects tagged player");

		for (int i = 0; i < players.Length; i++)
		{
			players[i] = playerObjs[i].GetComponent<PlayerHolder>();
			players[i].OnActionPress += PlayerPressAction;
		}
	}

	void PlayerPressAction(PlayerHolder player)
	{
		if (Vector3.Distance(transform.position, player.transform.position) < pickupRadius && player.GetPickupBox != this)
			Pickup(player);
	}

	public void Pickup(PlayerHolder player)
	{
		rb.isKinematic = true;
		col.enabled = false;
		player.SetPickupBox(this);
	}

	public void Throw()
	{
		rb.isKinematic = false;
		col.enabled = true;
	}

	protected virtual void Collide(Collision col)
	{
		
	}

	void OnCollisionEnter(Collision col)
	{
		Collide(col);
	}
}
