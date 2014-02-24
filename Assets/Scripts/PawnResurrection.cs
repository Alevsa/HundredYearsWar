using UnityEngine;
using System.Collections;

public class PawnResurrection : MonoBehaviour
{
	public GameObject healthBarPrefab;
	//public GameObject healthBarOutlinePrefab;
	private GameObject cloneHealthBar;
	//private GameObject cloneHealthBarOutline;

	public bool facingRight;
	public float setPawnHealth = 100f;
	public float pawnCurrentHealth;

	private float lastPikeHitTime;
	public float noPikeDamagePeriod = 0.5f;

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

	private SpriteRenderer healthBar;
	private Vector3 healthScale;	

	// Use this for initialization
	void Start () 
	{
		bishopCheckTop = transform.Find ("bishopCheckTop");
		bishopCheckBottom = transform.Find ("bishopCheckBottom");
		resurrectLoc = transform.Find ("resurrectLoc");
		oldLayer = gameObject.layer;
		player = GameObject.Find ("Bishop");
		resurrectTimeToGo = resurrectTime;

		if (!facingRight)
			Flip ();
	}

	void FixedUpdate()
	{
		bishopTop = Physics2D.Linecast(transform.position, bishopCheckTop.position, 1 << LayerMask.NameToLayer ("Player"));
		bishopBottom = Physics2D.Linecast(transform.position, bishopCheckBottom.position, 1 << LayerMask.NameToLayer ("Player"));

		if (carried) 
		{
			rigidbody2D.isKinematic = true;
			this.gameObject.layer = 12;
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + player.transform.lossyScale.y / 2);
		}

		if (!pawnAlive)
		{
		    rigidbody2D.isKinematic = true;
		    this.gameObject.layer = 12;
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

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Pike") 
		{
			  if (pawnCurrentHealth > 1f) 	
				{
				  pawnCurrentHealth -= 0.1f;
				  UpdateHealthBar();
				}
				else 
				{
				   pawnCurrentHealth -= 1f;
				   Destroy (cloneHealthBar);
				   //Destroy (cloneHealthBarOutline);
				   PawnDeath ();
				}
				
		 }
	    
	}

	void Resurrect()
	{
		rigidbody2D.isKinematic = false;
		resurrectTimeToGo = resurrectTime;
		pawnAlive = true;
		resurrecting = false;
		Destroy (cloneResurrect);
		pawnCurrentHealth = setPawnHealth;

		cloneHealthBar = (GameObject) Instantiate (healthBarPrefab, resurrectLoc.position, resurrectLoc.rotation);
		cloneHealthBar.transform.parent = transform;
		//cloneHealthBarOutline = (GameObject) Instantiate (healthBarOutlinePrefab, resurrectLoc.position, resurrectLoc.rotation);
		//cloneHealthBarOutline.transform.parent = transform;

		healthBar = transform.Find ("cloneHealthBar(Clone)").GetComponent<SpriteRenderer> ();
		healthScale = healthBar.transform.localScale;
	}

	void PawnDeath()
	{
		this.gameObject.layer = 12;
		rigidbody2D.isKinematic = true;
		resurrectTimeToGo = resurrectTime;
		pawnAlive = false;
		resurrecting = false;
		Destroy (cloneResurrect);
		pawnCurrentHealth = 0f;
	}

	public void UpdateHealthBar ()
	{

		if (pawnCurrentHealth > 50) 
			healthBar.color = Color.Lerp (Color.green, Color.yellow, 1 - pawnCurrentHealth * 0.01f);
		else
			healthBar.color = Color.Lerp (Color.yellow, Color.red, 1 - pawnCurrentHealth * 0.01f);
		
		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * pawnCurrentHealth * 0.01f, healthScale.y, 1);
	}

	void Flip ()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

