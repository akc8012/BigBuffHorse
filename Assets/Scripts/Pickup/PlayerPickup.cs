using UnityEngine;

public class PlayerPickup : MonoBehaviour {

	Interactable heldItem;
	Interactable seenItem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPickupStay(Collider pickup) {
		
	}

	public void OnPickupEnter(Collider pickup) {
		Debug.Log("I see package!");
	}
}
