using UnityEngine;
using System.Collections;

public class FrogBehaviour : MonoBehaviour 
{
	public float deathDuration = 60f;
	private bool currentlyDead = false;
	
	public bool facingRight = true;
	public float moveForce = 100f;
	public float maxSpeed = 2f;

	private float h;

	private Transform checker;
	private Transform PikeLoc;


	private bool flipFrog = false;

	public float health = 100f;
	public float noDamagePeriod = 2f;
	private float lastHitTime;
	
	private bool pawnClose = false;

	private int oldLayer;
	
	private Animator anim;

	private bool fell = false;

	public GameObject RightPikePrefab;
	public GameObject LeftPikePrefab;
	private GameObject cloneRightPike;
	private GameObject cloneLeftPike;

	private bool pikePresent = false;

	private bool attacking = false;

	void Start () 
	{
		PikeLoc = transform.Find ("PikeLoc");
		checker = transform.Find ("Checker");

		anim = GetComponent<Animator>();

		oldLayer = gameObject.layer;
	}

	void FixedUpdate () 
	{
		flipFrog = Physics2D.Linecast (transform.position, checker.position, 1<< LayerMask.NameToLayer ("Blocker"));
		pawnClose = Physics2D.Linecast (transform.position, checker.position, 1<< LayerMask.NameToLayer ("Ally"));

		anim.SetFloat("Speed", Mathf.Abs (h));

		if (facingRight && !pawnClose && !currentlyDead)
			h = 1;

		if (!facingRight && !pawnClose && !currentlyDead)
			h = -1;

		if (flipFrog) 
			Flip();

	    if (pawnClose) 
		{
			h = 0f;
			Attack ();
		}

		if(!pawnClose)
		{

		if(rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Pawn")
		{
			if(Time.time > lastHitTime + noDamagePeriod)
			{
			   if(health>20f)
		       {
				  lastHitTime = Time.time; 
			      health -= 20f;
			   }
			   else
			   {
				  health -=20f;
				  StartCoroutine(Death());
			   }
			}
		}
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Attack()
	{ 
		attacking = true;

		if (facingRight && attacking && !pikePresent) 
		{
			cloneRightPike = (GameObject) Instantiate (RightPikePrefab, PikeLoc.position, PikeLoc.rotation);
			pikePresent = true;
			attacking = false;
		}

		if (!facingRight && attacking &&!pikePresent) 
		{
			cloneLeftPike = (GameObject) Instantiate (LeftPikePrefab, PikeLoc.position, PikeLoc.rotation);
			pikePresent = true;
			attacking = false;
		}

		if (!pawnClose) 
		{
			Destroy (cloneRightPike);
			Destroy (cloneLeftPike);
			pikePresent = false;
		}

	}

    IEnumerator Death()
	{
		currentlyDead = true;
		h = 0f;
		rigidbody2D.isKinematic = true;
		Destroy (cloneRightPike);
		Destroy (cloneLeftPike);
		pikePresent = false;
		this.gameObject.layer = 10;
		if (!fell) 
		{
			this.transform.Rotate(0f, 0f, 90f);
			this.transform.position += new Vector3(0f, -0.5f, 0f);
			fell = true;
		}
		yield return new WaitForSeconds(deathDuration);
		this.gameObject.layer = oldLayer;
		rigidbody2D.isKinematic = false;
		health = 100f;
		if (fell) 
		{
			this.transform.Rotate(0f, 0f, -90f);
			this.transform.position += new Vector3(0f, 0.5f, 0f);
			fell = false;
		}
		currentlyDead = false;
	}
}
