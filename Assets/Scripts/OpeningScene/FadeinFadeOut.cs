using UnityEngine;
using System.Collections;

public class FadeinFadeOut : MonoBehaviour 
{
	public string LoadWhatLevel;
	public float FadeTime = 5f;
	public Texture FadeTexture;
	private float AlphaFadeValue = 1f;
	private float AlphaFadeOutValue = 0f;
	
	private bool OnTimeFadeIn = true;
	private bool OnTimeFadeOut = false;
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine (levelProgressing ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		if (OnTimeFadeIn) 
		{
			AlphaFadeValue = Mathf.Clamp01 (AlphaFadeValue - (Time.deltaTime / FadeTime));
			GUI.color = new Color (0, 0, 0, AlphaFadeValue);
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), FadeTexture);
		}
		
		if (OnTimeFadeOut) 
		{
			AlphaFadeOutValue = Mathf.Clamp01 (AlphaFadeOutValue + (Time.deltaTime / FadeTime));
			GUI.color = new Color (0, 0, 0, AlphaFadeOutValue);
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), FadeTexture);
		}
		
	}
	
	IEnumerator levelProgressing()
	{
		yield return new WaitForSeconds (10f);
		OnTimeFadeIn = false;
		OnTimeFadeOut = true;
		yield return new WaitForSeconds(3f);
		Application.LoadLevel (LoadWhatLevel);
	}

}
