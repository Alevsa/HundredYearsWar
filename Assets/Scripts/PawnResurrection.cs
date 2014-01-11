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
	private GameObject player;

	private bool carried = false;
	private int noCollisionLayer;
	private int oldLayer;

	// Use this for initialization
	void Start () 
	{
		bishopCheckTop = transform.Find ("bishopCheckTop");
		bishopCheckBottom = transform.Find ("bishopCheckBottom");
		resurrectLoc = transform.Find ("resurrectLoc");
		oldLayer = gameObject.layer;
		player = GameObject.Find ("Bishop");
		//noCollisionLayer = 1 << LayerMask.NameToLayer ("NoCollision");
	}

	void FixedUpdate()
	{
		bishopTop = Physics2D.Linecast(transform.position, bishopCheckTop.position, 1 << LayerMask.NameToLayer ("Player"));
		bishopBottom = Physics2D.Linecast(transform.position, bishopCheckBottom.position, 1 << LayerMask.NameToLayer ("Player"));

		if (carried) 
		{
			this.gameObject.layer = 10;
			transform.position = player.transform.position;
		}

		if (!pawnAlive)
		{
			rigidbody2D.isKinematic = true;
			this.gameObject.layer = 10;
		}
		else
		{
			rigidbody2D.isKinematic = false;
			this.gameObject.layer = oldLayer;
		}
	}

	void Update () 
	{	
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

		if ((bishopTop || bishopBottom) && pawnAlive && Input.GetButtonDown ("Fire2") && !carried)
			carried = true;
		else if (Input.GetButtonDown ("Fire2") && carried)
			carried = false;

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
