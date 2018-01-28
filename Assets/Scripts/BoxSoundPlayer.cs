using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSoundPlayer : MonoBehaviour
{

	void OnCollisionEnter(Collision col)
	{
		SoundManager.instance.PlayClip("boxSlam" + Random.Range(0, 4), 0.5f);
	}
}
