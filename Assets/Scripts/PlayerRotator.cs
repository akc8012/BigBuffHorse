using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
	[SerializeField] protected PlayerBallController ball;
	[SerializeField] protected float rotationSmooth = 15f;

	// Not all sticks work 100% This accomodates faulty controllers.
	[SerializeField] protected float gamepadError = 0.1f;     

	protected Vector3 input;
	protected Rigidbody rigidBody;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void LateUpdate()
	{
		input = ball.GetInput();

		transform.position = ball.transform.position;

		if (Mathf.Abs(input.x) > gamepadError || Mathf.Abs(input.z) > gamepadError)
			Rotate();
	}

	protected virtual void Rotate()
	{
		Quaternion tempRot = transform.rotation;
		tempRot.SetLookRotation(input, Vector3.up);

		//Smoothly rotate our player to face the direction it moves to drastically improve aesthetics.
		transform.rotation = Quaternion.Lerp(transform.rotation, tempRot, rotationSmooth * Time.deltaTime);
	}
}
