using UnityEngine;
using System.Collections;

public class OpeningSceneText : MonoBehaviour 
{

	public string firstText;
	public string secondText;
	public string thirdText;
	public string fourthText;
	public string fifthText;
	public string sixthText;
	public string seventhText;
	public float changeDelay;
	private string lineBreak = "\n";
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine (GUITextChange ());
	}
	
	IEnumerator GUITextChange()
	{
		GUIMessage(string.Format (firstText+lineBreak+secondText+lineBreak+thirdText+lineBreak+lineBreak+fourthText+lineBreak+fifthText+lineBreak+sixthText+lineBreak+seventhText));
		yield return new WaitForSeconds(changeDelay);
	}
	
	public void GUIMessage(string message)
	{
		guiText.text = message;
	}

}


