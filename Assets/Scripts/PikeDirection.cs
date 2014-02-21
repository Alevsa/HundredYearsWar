using UnityEngine;
using System.Collections;

public class PikeDirection : MonoBehaviour 
{
	public bool PikeLeft;

	private Animator anim;


	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();

		if (PikeLeft)
			Flip ();
	}

	void FixedUpdate () 
	{
		anim.SetBool("PikeLeft", PikeLeft);	
	}

	void Flip ()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
