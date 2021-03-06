﻿using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	[SerializeField] protected int points;

	[Header("Spring settings")]
	[SerializeField] protected float spring;
	[SerializeField] protected float damper;
	[SerializeField] protected float minDistance;
	[SerializeField] protected float maxDistance;

	[Header("IK settings")]
	[SerializeField] protected Transform LHand;
	[SerializeField] protected Transform RHand;

	public int GetPoints { get { return points; } }

	public bool scored = false;
	public float lastVelocity;
	protected Rigidbody rigidBody;
	protected bool held = false;
	protected Transform holdingPlayer;

	protected virtual void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	protected virtual void LateUpdate()
	{
		lastVelocity = rigidBody.velocity.magnitude;
	}

	// Function to be triggered when player picks up this object.
	// I.E., bomb starts fuse.
	public virtual void OnPickup(Transform player)
	{
		held = true;
		holdingPlayer = player;

		// Create and setup new spring joint
		SpringJoint springJoint = gameObject.AddComponent<SpringJoint>();
		springJoint.autoConfigureConnectedAnchor = false;
		springJoint.connectedBody = player.GetComponentInChildren<Rigidbody>();
		springJoint.connectedAnchor = Vector3.zero;
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.minDistance = minDistance;
		springJoint.maxDistance = maxDistance;

		// Attach player IK to self
		IKControl ik = player.GetComponentInChildren<IKControl>();
		ik.leftHandObj = LHand;
		ik.rightHandObj = RHand;
		ik.ikActive = true; 
	}

	public virtual void OnDrop(Transform player)
	{
		held = false;
		holdingPlayer = null;

		IKControl ik = player.GetComponentInChildren<IKControl>();	
		ik.ikActive = false;
		ik.leftHandObj = null;
		ik.rightHandObj = null;

		Destroy(gameObject.GetComponent<SpringJoint>());
	}

	public virtual void OnThrow(Transform player)
	{
		held = false;
		holdingPlayer = null;

		OnDrop(transform);
	}

	public virtual void Destroy() {
		Destroy(gameObject, 1);
	}
}
