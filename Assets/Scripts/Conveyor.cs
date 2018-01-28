using UnityEngine;

public class Conveyor : MonoBehaviour
{
	Animator[] animators;

	void Start() {
		animators = transform.GetComponentsInChildren<Animator>();
		foreach (Animator anim in animators)
		{
			anim.enabled = false;
		}
	}

	public void StartRollers() {
		foreach (Animator anim in animators)
		{
			anim.enabled = true;
		}
	}
}
