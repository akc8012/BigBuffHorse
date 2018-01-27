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

			Quaternion fromToRot = 
		}
	}
}
