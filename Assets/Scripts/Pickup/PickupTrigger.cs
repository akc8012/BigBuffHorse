using UnityEngine;

public class PickupTrigger : MonoBehaviour {

	[SerializeField] PlayerPickup pickup;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Interactable")
		{
			pickup.OnPickupEnter(other);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Interactable") {
			pickup.OnPickupStay(other);
		}
	}
}
