using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField] AudioClip explodeSound;
	[SerializeField] float explodeForce = 10f;

	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
		audioSource.PlayOneShot(explodeSound);
		Destroy(gameObject, 0.75f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Boxes" || other.gameObject.tag == "Debris" || other.gameObject.tag == "Player")
		{
			Vector3 explodeDir = Vector3.Normalize(other.transform.position - transform.position);
			other.GetComponent<Rigidbody>().AddForce(explodeDir * explodeForce, ForceMode.Impulse);

			if (other.gameObject.tag == "Player")
				other.GetComponent<PlayerFallDown>().FallDown();
		}
	}
}
