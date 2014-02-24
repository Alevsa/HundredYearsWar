using UnityEngine;
using System.Collections;

public class HealthUpdater : MonoBehaviour 
{

	private GameObject bishop;
	private BishopHealthMain bishopHealth;
	
	void Start () 
	{
		bishop = GameObject.Find ("Bishop");
		bishopHealth = bishop.GetComponent<BishopHealthMain> ();
	}

	void FixedUpdate () 
	{
		float f = Mathf.FloorToInt(bishopHealth.BishopCurrentHealth);
		guiText.text = f.ToString ();
	}
}
