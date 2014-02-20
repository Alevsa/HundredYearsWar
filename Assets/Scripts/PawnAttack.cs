using UnityEngine;
using System.Collections;

public class PawnAttack : MonoBehaviour 
{
	private bool FrogPresent = false;
	private Transform FrogCheck;

	private Animator anim;

	private bool attack = false;

	// Use this for initialization
	void Start () 
	{
		FrogCheck = transform.Find ("frogCheck");
		anim = GetComponent<Animator>();
	}

	void FixedUpdate () 
	{
		anim.SetBool("Attack", attack);
		FrogPresent = Physics2D.Linecast (transform.position, FrogCheck.position, 1<< LayerMask.NameToLayer ("Ground"));

		if (FrogPresent && (this.gameObject.layer == 11)) 
			attack = true;
		else
			attack = false;

	}
}
