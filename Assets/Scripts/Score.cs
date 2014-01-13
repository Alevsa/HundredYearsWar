using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
	{
		public float Timer = 100;							
		
		void Awake ()
		{
		}
		
		void Update ()
		{
		Timer -= Time.deltaTime;
			// Set the score text.
			guiText.text = "Timer: " + Timer;
		}
		
	}

