using UnityEngine;

public class Conveyor : MonoBehaviour
{
	Animator[] animators;

	void Start() {
		GetAnimators();
		StopRollers();

		GameStateManager.instance.OnRoundStart += StartRollers;
		GameStateManager.instance.OnRoundEnd += StopRollers;
	}

	void OnDestroy()
	{
		GameStateManager.instance.OnRoundStart -= StartRollers;
		GameStateManager.instance.OnRoundEnd -= StopRollers;
	}

	void GetAnimators()
	{
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

	public void StopRollers()
	{
		foreach (Animator anim in animators)
		{
			anim.enabled = false;
		}
	}
}
