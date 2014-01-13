using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
	public float timeRemaining = 100;							
	
	void Awake ()
	{
	}
	
	void Update ()
	{
		timeRemaining -= Time.deltaTime;
		// Set the score text.
		guiText.text = "Timer: " + timeRemaining;
	}
	
}

