using UnityEngine;
using System.Collections;

public class FrogSpawner : MonoBehaviour 
{
	public float spawnTime = 50f;		// The amount of time between each spawn.
	public float spawnDelay = 20f;		// The amount of time before spawning starts.
	public GameObject[] frogs;		// Array of enemy prefabs.	
	
	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, frogs.Length);
		Instantiate(frogs[enemyIndex], transform.position, transform.rotation);
	}

}
