using UnityEngine;
using System.Collections;

public class PawnAttack : MonoBehaviour 
{
	private bool FrogPresent = false;
	private Transform FrogCheck;

	private Animator anim;
	private PawnResurrection pawnRes;

	private bool attack = false;
	private bool leftAttack = false;

	// Use this for initialization
	void Start () 
	{
		FrogCheck = transform.Find ("frogCheck");
		anim = GetComponent<Animator>();
		pawnRes = GetComponent<PawnResurrection> ();
	}

	void FixedUpdate () 
	{
		anim.SetBool("Attack", attack);
		anim.SetBool ("leftAttack", leftAttack);

		FrogPresent = Physics2D.Linecast (transform.position, FrogCheck.position, 1<< LayerMask.NameToLayer ("Enemy"));

		if (FrogPresent && (this.gameObject.layer == 12)) {
						if (pawnRes.facingRight == true)
								attack = true;
						else
								leftAttack = true;
				} else {
						attack = false;
						leftAttack = false;
				}

	}
}
