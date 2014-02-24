using UnityEngine;
using System.Collections;

public class FrogSpawner : MonoBehaviour 
{
	public float spawnTime = 50f;		// The amount of time between each spawn.
	public float spawnDelay = 20f;		// The amount of time before spawning starts.
	public GameObject frog;	
	public bool spawn = true;
	
	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	void Spawn ()
	{
		if(spawn)
		Instantiate(frog, transform.position, transform.rotation);
	}

}
