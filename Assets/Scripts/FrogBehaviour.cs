using UnityEngine;
using System.Collections;

public class FrogBehaviour : MonoBehaviour 
{
	public GameObject healthBarPrefab;
	//public GameObject healthBarOutlinePrefab;
	private GameObject cloneHealthBar;
	//private GameObject cloneHealthBarOutline;
	
	public float deathDuration = 60f;
	private bool currentlyDead = false;
	
	public bool facingRight = true;
	public float moveForce = 100f;
	public float maxSpeed = 2f;

	private float h;

	private Transform checker;
	private Transform PikeLoc;
	private Transform OtherFrogCheck;
	private Transform healthBarLoc;

	private bool flipFrog = false;

	public float health = 100f;
	public float noDamagePeriod = 2f;
	private float lastHitTime;
	
	private bool pawnClose = false;
	private bool bishopClose = false;

	private int oldLayer;
	
	private Animator anim;

	private bool fell = false;

	public GameObject RightPikePrefab;
	public GameObject LeftPikePrefab;
	private GameObject cloneRightPike;
	private GameObject cloneLeftPike;

	private bool pikePresent = false;

	private bool attacking = false;

	private SpriteRenderer healthBar;
	private Vector3 healthScale;

	private bool Respawned = false;

	void Start () 
	{
		PikeLoc = transform.Find ("PikeLoc");
		checker = transform.Find ("Checker");
		OtherFrogCheck = transform.Find ("OtherFrogCheck");
		healthBarLoc = transform.Find ("HealthBar");

		CreateHealthBar ();

		anim = GetComponent<Animator>();

		oldLayer = gameObject.layer;
	}

	void FixedUpdate () 
	{
		flipFrog = Physics2D.Linecast (checker.position, OtherFrogCheck.position, 1<< LayerMask.NameToLayer ("Blocker"));
		pawnClose = Physics2D.Linecast (transform.position, checker.position, 1<< LayerMask.NameToLayer ("Ally"));
		bishopClose = Physics2D.Linecast (transform.position, checker.position, 1<< LayerMask.NameToLayer ("Player"));

		anim.SetFloat("Speed", Mathf.Abs (h));

		if (facingRight && !pawnClose && !currentlyDead)
			h = 1;

		if (!facingRight && !pawnClose && !currentlyDead)
			h = -1;

		if (flipFrog) 
			Flip();

	    if ((pawnClose || bishopClose) && !currentlyDead) 
		{
			h = 0f;
			Attack ();
		}

		if(!pawnClose &&  !bishopClose)
		{

		if(rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}

		if (!pawnClose && !bishopClose) 
		{
			Destroy (cloneRightPike);
			Destroy (cloneLeftPike);
			pikePresent = false;
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
				  UpdateHealthBar();
			   }
			   else
			   {
				  health -=20f;
				  Destroy (cloneHealthBar);
				 // Destroy (cloneHealthBarOutline);
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
			cloneRightPike.transform.parent = transform;
			pikePresent = true;
			attacking = false;
		}

		if (!facingRight && attacking &&!pikePresent) 
		{
			cloneLeftPike = (GameObject) Instantiate (LeftPikePrefab, PikeLoc.position, PikeLoc.rotation);
			cloneLeftPike.transform.parent = transform;
			pikePresent = true;
			attacking = false;
		}

		if (!pawnClose && !bishopClose) 
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
		this.gameObject.layer = 12;
		if (!Respawned) {
						if (!fell) {
								this.transform.Rotate (0f, 0f, 90f);
								this.transform.position += new Vector3 (0f, -0.5f, 0f);
								fell = true;
						}
						yield return new WaitForSeconds (deathDuration);
						this.gameObject.layer = oldLayer;
						rigidbody2D.isKinematic = false;
						health = 100f;
						CreateHealthBar ();
						if (fell) {
								this.transform.Rotate (0f, 0f, -90f);
								this.transform.position += new Vector3 (0f, 0.5f, 0f);
								fell = false;
						}
						currentlyDead = false;
				}
	}

	void CreateHealthBar()
	{
		cloneHealthBar = (GameObject) Instantiate (healthBarPrefab, healthBarLoc.position, healthBarLoc.rotation);
		cloneHealthBar.transform.parent = transform;
		//cloneHealthBarOutline = (GameObject) Instantiate (healthBarOutlinePrefab, healthBarLoc.position, healthBarLoc.rotation);
		//cloneHealthBarOutline.transform.parent = transform;
		healthBar = transform.Find ("cloneHealthBar(Clone)").GetComponent<SpriteRenderer> ();
		healthScale = healthBar.transform.localScale;
	}

	void UpdateHealthBar ()
	{

		if (health > 50) 
			healthBar.color = Color.Lerp (Color.green, Color.yellow, 1 - health * 0.01f);
		else
			healthBar.color = Color.Lerp (Color.yellow, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, healthScale.y, 1);
	}
}
