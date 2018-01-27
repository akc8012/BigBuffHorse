using UnityEngine;

public enum HoldState { Idle, Holding, Tugging }

public class PlayerHolder : MonoBehaviour
{
	PlayerBallController controller;
	PlayerRotator rotator;
	PickupBox pickupBox = null;
	HoldState holdState = HoldState.Idle;

	public HoldState GetState { get { return holdState; } }
	public PickupBox GetPickupBox { get { return pickupBox; } }

	public delegate void ActionPress(PlayerHolder player);
	public event ActionPress OnActionPress;

	void Awake()
	{
		controller = GetComponent<PlayerBallController>();
		rotator = GetComponentInChildren<PlayerRotator>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Action" + controller.GetPlayerNdx))
		{
			if (OnActionPress != null)
				OnActionPress(this);

			switch (holdState)
			{
				case HoldState.Idle:

				break;

				case HoldState.Holding:

				break;

				case HoldState.Tugging:

				break;
			}
		}
	}

	public void SetPickupBox(PickupBox pickupBox)
	{
		this.pickupBox = pickupBox;
		pickupBox.transform.parent = rotator.transform;
	}
}
