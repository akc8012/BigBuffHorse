using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTarget : MonoBehaviour
{
	[SerializeField] ScoreDisplay scoreDisplay;
	[SerializeField] float destroyTimer = 1;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Interactable")
		{
			Interactable box = col.GetComponent<Interactable>();
			scoreDisplay.AddScore(col.GetComponent<Interactable>().GetPoints);
			StartCoroutine(DestroyBoxAfterTime(box, destroyTimer));
		}
	}

	IEnumerator DestroyBoxAfterTime(Interactable box, float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(box.gameObject);
	}
}
