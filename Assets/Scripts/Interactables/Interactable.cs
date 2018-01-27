using UnityEngine;

public enum InteractableSize
{
	Small,
	Medium,
	Large
}

public abstract class Interactable : MonoBehaviour
{
	[SerializeField] InteractableSize size;

	// Function to be triggered when player picks up this object.
	// I.E., bomb starts fuse.
	public abstract void OnPickup();
}
