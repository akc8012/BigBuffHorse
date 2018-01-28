using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{
	Animator anim;

	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
	}

	public void Play()
	{
		anim.SetTrigger("Play");
		SoundManager.instance.PlayClip("whistle");
		StartCoroutine(WaitForEndAnim());
	}

	IEnumerator WaitForEndAnim()
	{
		yield return new WaitForSeconds(2.25f);
		ScoreManager.instance.RoundEnd();
	}
}
