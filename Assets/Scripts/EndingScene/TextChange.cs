using UnityEngine;
using System.Collections;

public class TextChange : MonoBehaviour 
{

	public string firstText;
	public string secondText;
	public string thirdText;
	public string fourthText;
	public string fifthText;
	public float changeDelay;
	private string lineBreak = "\n";

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (GUITextChange ());
	
	}

	IEnumerator GUITextChange()
	{
		GUIMessage (firstText);
		yield return new WaitForSeconds(changeDelay);
		GUIMessage(secondText);
		yield return new WaitForSeconds(changeDelay);
		guiText.fontSize = 40;
		GUIMessage(string.Format (thirdText+lineBreak+fourthText+lineBreak+fifthText));
	}

	public void GUIMessage(string message)
	{
		guiText.text = message;
	}

}
