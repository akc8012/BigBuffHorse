using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
	[SerializeField] int playerNdx = 0;
	[SerializeField] float speedModifier = 1.0f;
	[SerializeField] float collisionBounceModifier = 1.0f;

	const float MaxVelocity = 7.3f;
	const float VelocityDampen = 0.98f;

	Camera cam;
	Rigidbody rb;
	Vector3 lastPos;
	Vector3 distanceInFrame;

	public Vector3 Distance { get { return distanceInFrame; } }
	public int GetPlayerNdx { get { return playerNdx; } }
	public float GetSpeed { get { Vector3 speed = rb.velocity; speed.y = 0; return speed.magnitude; } }
	public float GetNormalizedSpeed { get { Vector3 speed = rb.velocity; speed.y = 0; return speed.magnitude / MaxVelocity; } }

	Vector3 input;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		cam = Camera.main;
	}

	void FixedUpdate()
	{
		input = new Vector3(Input.GetAxisRaw("Horizontal" + playerNdx), 0, Input.GetAxisRaw("Vertical" + playerNdx));
		Vector3 movement = GetCameraRelativeMovement(cam.transform, input);

		rb.AddForce(Vector3.down, ForceMode.VelocityChange);

		rb.AddForce(movement * speedModifier, ForceMode.VelocityChange);
		if (GetSpeed > MaxVelocity)
			rb.velocity *= VelocityDampen;

		Vector3 potentialDistance = transform.position - lastPos;
		if (potentialDistance.magnitude > 0.01f)
			distanceInFrame = potentialDistance;

		lastPos = transform.position;
	}

	Vector3 GetCameraRelativeMovement(Transform cam, Vector3 input)
	{
		float speed = Mathf.Clamp(Vector3.Magnitude(input), 0, 1);

		Vector3 cameraDir = cam.forward; cameraDir.y = 0.0f;
		Vector3 moveDir = Quaternion.FromToRotation(Vector3.forward, cameraDir) * input;

		return moveDir.normalized * speed;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Floor")
			return;

		rb.AddForce(col.contacts[0].normal * collisionBounceModifier, ForceMode.VelocityChange);
	}

	public Vector3 GetInput() {
		return input;
	}
}
