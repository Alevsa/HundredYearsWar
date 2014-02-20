using UnityEngine;
using System.Collections;

public class OtherControls : MonoBehaviour 
{
	private bool gamePaused = false;
	private bool VolumeOn = true;

	// Use this for initialization
	

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			if(!gamePaused)
			{
				gamePaused = true;
				guiText.text = string.Format("Paused\n\nControls\nW,A,S,D - Move, Spacebar - Jump\nQ - Pick up Pawn, Left Mouse - Resurrect Pawn\n M - Mute Music");
				Time.timeScale = 0;
				if (VolumeOn)
				audio.Pause ();
			}
			else
			{
				gamePaused = false;
				guiText.text = string.Format("");
				Time.timeScale = 1;

				if(VolumeOn)
				audio.Play();

			}
		}

		if (Input.GetKeyDown (KeyCode.M)) 
		{
			if (!gamePaused) 
			{
				if (VolumeOn) 
				{
					VolumeOn = false;
					 audio.Pause ();
				} 

				else 
				{
				    VolumeOn = true;
					audio.Play ();
				}
		    }
		}
	}
}
