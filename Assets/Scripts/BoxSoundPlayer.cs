using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSoundPlayer : MonoBehaviour
{
	bool canPlay = true;
	const float TimeDelay = 2;

	void OnCollisionEnter(Collision col)
	{
		if (canPlay)
		{
			SoundManager.instance.PlayClip("boxSlam" + Random.Range(0, 4), 0.5f);
			canPlay = false;
			StartCoroutine(SoundTimer());
		}
	}

	IEnumerator SoundTimer()
	{
		yield return new WaitForSeconds(TimeDelay);
		canPlay = true;
	}

}
