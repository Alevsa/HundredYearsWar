using UnityEngine;
using System.Collections;

public class PawnResurrection : MonoBehaviour
{
	public bool facingLeft;
	public float PawnHealth = 0f;

	public float resurrectTime = 3f;
	private float resurrectTimeToGo;

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
		resurrectTimeToGo = resurrectTime;
	}

	void FixedUpdate()
	{
		bishopTop = Physics2D.Linecast(transform.position, bishopCheckTop.position, 1 << LayerMask.NameToLayer ("Player"));
		bishopBottom = Physics2D.Linecast(transform.position, bishopCheckBottom.position, 1 << LayerMask.NameToLayer ("Player"));

		if (carried) 
		{
			rigidbody2D.isKinematic = true;
			this.gameObject.layer = 10;
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + player.transform.lossyScale.y / 2);
		}

		if (!pawnAlive)
		{
		    rigidbody2D.isKinematic = true;
		    this.gameObject.layer = 10;
		}
		else if (!carried)
		{
			rigidbody2D.isKinematic = false;
			this.gameObject.layer = oldLayer;
		}
	}

	void Update () 
	{	
	    if ((bishopTop || bishopBottom) && Input.GetButton("Fire1") && !pawnAlive) 
		{
		  resurrectTimeToGo -= Time.deltaTime;

	      if(!resurrecting)
			{
			  resurrecting = true;
		      cloneResurrect = (GameObject) Instantiate (ResurrectPrefab, resurrectLoc.position, resurrectLoc.rotation);
			}

			if(resurrectTimeToGo < 0)
		       Resurrect ();
	    }

		if((!bishopTop && !bishopBottom) || Input.GetButtonUp("Fire1"))
		{
			resurrectTimeToGo = resurrectTime;
			resurrecting = false;
			Destroy (cloneResurrect);
		}

		if ((bishopTop || bishopBottom) && pawnAlive && Input.GetButtonDown ("Fire2") && !carried)
		{
			carried = true;
			transform.Rotate (new Vector3(0, 0, 90));
		}
		else if (Input.GetButtonDown ("Fire2") && carried)
		{
			carried = false;
			transform.Rotate (new Vector3(0, 0, -90));
			transform.position = new Vector3 (player.transform.position.x + player.transform.lossyScale.x / 2, player.transform.position.y);
		}

	}

	void Resurrect()
	{
		rigidbody2D.isKinematic = false;
		resurrectTimeToGo = resurrectTime;
		pawnAlive = true;
		resurrecting = false;
		Destroy (cloneResurrect);
		PawnHealth = 100f;
	}
}
