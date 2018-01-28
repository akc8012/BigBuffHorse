using UnityEngine;

public class Chute : MonoBehaviour
{
	[SerializeField] GameObject[] spawnables;
	[SerializeField] float minSpawnDelay;
	[SerializeField] float maxSpawnDelay;

	float spawnTimer;

	Transform spawnLoc;
	Transform boxContainer;

	void Awake()
	{
		spawnLoc = transform.FindChild("Spawn Loc");
		boxContainer = GameObject.Find("Boxes").transform;
		SetNextSpawnTime();
	}

	void Update()
	{
		if (Time.time >= spawnTimer && GameStateManager.instance.GetState == GameState.Playing)
		{
			SpawnObject();
			SetNextSpawnTime();
		}
	}

	public void SpawnObject()
	{
		int randNum = Random.Range(0, spawnables.Length);
		Instantiate(spawnables[randNum], spawnLoc.position, Quaternion.identity, boxContainer);
	}

	void SetNextSpawnTime()
	{
		spawnTimer = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
	}
}
