using UnityEngine;
using System.Collections;

public class PawnResurrection : MonoBehaviour
{
	public float PawnHealth = 0f;

	public float resurrectTime = 5f;

	private bool resurrecting;
	private float startTime;

	private bool pawnAlive = false;
	private bool bishopTop = false;
	private bool bishopBottom = false;

	private Transform bishopCheckTop;
	private Transform bishopCheckBottom;

	private Transform resurrectLoc;
	public GameObject ResurrectPrefab;
	private GameObject cloneResurrect;

	// Use this for initialization
	void Start () 
	{
		bishopCheckTop = transform.Find ("bishopCheckTop");
		bishopCheckBottom = transform.Find ("bishopCheckBottom");
		resurrectLoc = transform.Find ("resurrectLoc");
	}

	void Update () 
	{
		bishopTop = Physics2D.Linecast(transform.position, bishopCheckTop.position, 1 << LayerMask.NameToLayer ("Player"));
		bishopBottom = Physics2D.Linecast(transform.position, bishopCheckBottom.position, 1 << LayerMask.NameToLayer ("Player"));
	
	    if ((bishopTop || bishopBottom) && Input.GetButton("Fire1") && !pawnAlive) 
		{
		  resurrectTime -= Time.deltaTime;

	      if(!resurrecting)
			{
			  resurrecting = true;
		      cloneResurrect = (GameObject) Instantiate (ResurrectPrefab, resurrectLoc.position, resurrectLoc.rotation);
			}

			if(resurrectTime < 0)
		       Resurrect ();
	    }

		if((!bishopTop && !bishopBottom) || !Input.GetButton("Fire1"))
		{
			resurrectTime = 5f;
			resurrecting = false;
			Destroy (cloneResurrect);
		}

	}

	void Resurrect()
	{
		resurrectTime = 5f;
		pawnAlive = true;
		resurrecting = false;
		Destroy (cloneResurrect);
		PawnHealth = 100f;
		transform.Rotate(new Vector3(0, 0, -90));
	}
}
