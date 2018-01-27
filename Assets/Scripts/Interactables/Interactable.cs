using UnityEngine;

public enum InteractableSize
{
	Small,
	Medium,
	Large
}

public abstract class Interactable : MonoBehaviour
{
	[SerializeField] protected int points;
	[SerializeField] protected InteractableSize size;

	[Header("Spring settings")]
	[SerializeField] protected float spring;
	[SerializeField] protected float damper;
	[SerializeField] protected float minDistance;
	[SerializeField] protected float maxDistance;

	public int GetPoints { get { return points; } }

	// Function to be triggered when player picks up this object.
	// I.E., bomb starts fuse.
	public virtual void OnPickup(Transform player)
	{
		// Create and setup new spring joint
		SpringJoint springJoint = gameObject.AddComponent<SpringJoint>();
		springJoint.connectedBody = player.GetComponentInChildren<Rigidbody>();
		springJoint.connectedAnchor = Vector3.zero;
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.minDistance = minDistance;
		springJoint.maxDistance = maxDistance;
	}
}
