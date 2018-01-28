using UnityEngine;

public class BoxTarget : MonoBehaviour
{
	[SerializeField] ScoreDisplay scoreDisplay;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Interactable")
		{
			Interactable box = col.GetComponent<Interactable>();
			if (!box.scored)
			{
				scoreDisplay.AddScore(col.GetComponent<Interactable>().GetPoints);
				box.Destroy();
			}
		}
	}
}
