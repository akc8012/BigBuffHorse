using UnityEngine;

public class Bomb : Interactable
{
	[Header("Bomb Settings")]
	[SerializeField] float explodeDelay;
	[SerializeField] GameObject explodePrefab;
	[SerializeField] AudioClip hissSound;

	AudioSource audioSource;

	protected override void Awake()
	{
		base.Awake();
		audioSource = GetComponent<AudioSource>();
	}

	public override void OnPickup(Transform player)
	{
		holdingPlayer = player;
		base.OnPickup(player);
		Invoke("Explode", explodeDelay);
		audioSource.PlayOneShot(hissSound);
	}

	protected void Explode()
	{
		Instantiate(explodePrefab, transform.position, Quaternion.identity);

		if(held)
			OnDrop(holdingPlayer);

		Destroy(gameObject);
	}
}
