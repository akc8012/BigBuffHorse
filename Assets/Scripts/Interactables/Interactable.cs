using UnityEngine;

public enum InteractableSize
{
	Small,
	Medium,
	Large
}

public abstract class Interactable : MonoBehaviour
{
	[SerializeField] protected InteractableSize size;
	[SerializeField] protected float anchorDistance;

	protected Transform anchorPoint;
	protected bool held;

	// Function to be triggered when player picks up this object.
	// I.E., bomb starts fuse.
	public virtual void OnPickup(Transform player)
	{
		SetAnchorPoint(player);
		held = true;
	}

	public void SetAnchorPoint(Transform anchorPoint)
	{
		this.anchorPoint = anchorPoint;
	}

	protected void LateUpdate()
	{
		if (held)
		{		
			float distanceToMove = Vector3.Distance(transform.position, anchorPoint.position) - anchorDistance;

			if (distanceToMove > 0)
			{
				Quaternion originalRot = transform.rotation;

				Vector3 dirToPlayer = Vector3.Normalize(anchorPoint.position - transform.position);
				Quaternion tempRot = transform.rotation;
				tempRot.SetLookRotation(dirToPlayer, Vector3.up);

				transform.position += transform.forward * distanceToMove;

				transform.rotation = originalRot;
			}
		}
	}
}
