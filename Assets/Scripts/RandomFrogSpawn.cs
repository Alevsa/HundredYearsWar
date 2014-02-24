using UnityEngine;
using System.Collections;

public class RandomFrogSpawn : MonoBehaviour 
{
	public GameObject frog;
	
	private GameObject frogSpawn1;
	private GameObject frogSpawn2;
	private GameObject frogSpawn3;
	private GameObject frogSpawn4;

	private Transform frogSpawn1Loc;
	private Transform frogSpawn2Loc;
	private Transform frogSpawn3Loc;
	private Transform frogSpawn4Loc;
	
	private FrogSpawner frogSpawner1;
	private FrogSpawner frogSpawner2;
	private FrogSpawner frogSpawner3;
	private FrogSpawner frogSpawner4;

	private int i;

	void Start () 
	{
		frogSpawn1 = GameObject.Find ("FrogSpawn1");
		frogSpawn2 = GameObject.Find ("FrogSpawn2");
		frogSpawn3 = GameObject.Find ("FrogSpawn3");
		frogSpawn4 = GameObject.Find ("FrogSpawn4");

		frogSpawn1Loc = frogSpawn1.transform;
		frogSpawn2Loc = frogSpawn2.transform;
		frogSpawn3Loc = frogSpawn3.transform;
		frogSpawn4Loc = frogSpawn4.transform;

		frogSpawner1 = frogSpawn1.GetComponent<FrogSpawner> ();
		frogSpawner2 = frogSpawn2.GetComponent<FrogSpawner> ();
		frogSpawner3 = frogSpawn3.GetComponent<FrogSpawner> ();
		frogSpawner4 = frogSpawn4.GetComponent<FrogSpawner> ();

		StartCoroutine (RandomSpawning ());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	IEnumerator RandomSpawning()
	{
		yield return new WaitForSeconds (60f);
		frogSpawner1.enabled = false;
		frogSpawner2.enabled = false;
		frogSpawner3.enabled = false;
		frogSpawner4.enabled = false;
		yield return new WaitForSeconds (30f);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (30f);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (1f);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (30f);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (1f);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (30f);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (1f);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (30f);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (1f);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (30f);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (1f);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (1f);
		SpawnFrogAtLocation(i);
		yield return new WaitForSeconds (1f);
		i = Random.Range (1, 4);
		SpawnFrogAtLocation(i);
	
	}

	void SpawnFrogAtLocation(int location)
	{
	switch (location)
		{
		case 1:
		    Instantiate(frog, frogSpawn1Loc.position, frogSpawn1Loc.rotation);
			break;
		case 2:
			Instantiate(frog, frogSpawn2Loc.position, frogSpawn2Loc.rotation);
			break;
		case 3:
			Instantiate(frog, frogSpawn3Loc.position, frogSpawn3Loc.rotation);
			break;
		case 4:
			Instantiate(frog, frogSpawn4Loc.position, frogSpawn4Loc.rotation);
			break;
		default:
			break;
	   }
   }
}