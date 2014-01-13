using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
	public float TimeRemaining = 300f;
	private float minutesRemaining;
	private float secondsRemaining;
	
	void Awake ()
	{
	}
	
	void Update ()
	{
		TimeRemaining -= Time.deltaTime;
		minutesRemaining = TimeRemaining / 60;
		secondsRemaining = TimeRemaining % 60;
		guiText.text = string.Format("Time Remaining: {0:00}m:{1:00}s", minutesRemaining, secondsRemaining);
	}
	
}

