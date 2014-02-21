using UnityEngine;
using System.Collections;

public class LevelFade : MonoBehaviour 
{
	public float FadeTime = 5f;
	public Texture FadeTexture;
	private float AlphaFadeValue = 1f;
//	private float AlphaFadeOutValue = 0f;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		AlphaFadeValue = Mathf.Clamp01 (AlphaFadeValue - (Time.deltaTime / FadeTime));
		GUI.color = new Color (0, 0, 0, AlphaFadeValue);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), FadeTexture);
	}
}
