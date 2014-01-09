using UnityEngine;
using System.Collections;

public class PawnResurrection : MonoBehaviour
{
	public float PawnHealth = 0f;

	private bool bishopTop = false;
	private bool bishopBottom = false;

	private Transform bishopCheckTop;
	private Transform bishopCheckBottom;

	private Transform resurrectLoc;

	// Use this for initialization
	void Start () 
	{
		bishopCheckTop = transform.Find ("bishopCheckTop");
		bishopCheckBottom = transform.Find ("bishopCheckBottom");
		resurrectLoc = transform.Find ("resurrectLoc");
	}

	// Update is called once per frame
	void Update () 
	{
		bishopTop = Physics2D.Linecast(transform.position, bishopCheckTop.position, 1 << LayerMask.NameToLayer ("Player"));
		bishopBottom = Physics2D.Linecast(transform.position, bishopCheckBottom.position, 1 << LayerMask.NameToLayer ("Player"));
	}

	void FixedUpdate()
	{
		if((bishopTop || bishopBottom) && Input.GetButtonDown("Fire1") && PawnHealth == 0f)
			Resurrect ();
	}

	void Resurrect()
	{
		PawnHealth = 100f;
		transform.Rotate(new Vector3(0, 0, -90));
	}
}
