using UnityEngine;

public enum InteractableSize
{
	Small,
	Medium,
	Large
}

public class Interactable : MonoBehaviour
{
	[SerializeField] InteractableSize size;


}
