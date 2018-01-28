using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
	[SerializeField] protected PlayerBallController ball;
	[SerializeField] protected float rotationSmooth = 15f;
	[SerializeField] protected float movementLag = 0.1f;

	// Not all sticks work 100% This accomodates faulty controllers.
	[SerializeField] protected float gamepadError = 0.1f;     

	protected Vector3 input;
	protected Rigidbody rigidBody;

	[SerializeField] Animator animator;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void LateUpdate()
	{
		input = ball.GetInput();
		transform.position = ball.transform.position;

		animator.SetFloat("Horizontal", input.x);
		animator.SetFloat("Vertical", input.z);		

		if (Mathf.Abs(input.x) > gamepadError || Mathf.Abs(input.z) > gamepadError)
		{
			Rotate();
			animator.SetTrigger("StartMoving");
		}
		else
		{
			animator.SetTrigger("StopMoving");
		}
	}

	protected virtual void Rotate()
	{
		Quaternion tempRot = transform.rotation;
		tempRot.SetLookRotation(input, Vector3.up);

		//Smoothly rotate our player to face the direction it moves to drastically improve aesthetics.
		transform.rotation = Quaternion.Lerp(transform.rotation, tempRot, rotationSmooth * Time.deltaTime);
	}
}
