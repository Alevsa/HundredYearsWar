using UnityEngine;
using System.Collections;

public class FrogBehaviour : MonoBehaviour 
{
	public bool facingRight = true;
	public float moveForce = 100f;
	public float maxSpeed = 2f;

	private float h;

	private Transform checker;
	private bool flipFrog = false;

	void Start () 
	{
		checker = transform.Find ("Checker");
	}

	void FixedUpdate () 
	{
		flipFrog = Physics2D.Linecast (transform.position, checker.position, 1<< LayerMask.NameToLayer ("Blocker"));

		if (facingRight)
			h = 1;

		if (!facingRight)
			h = -1;

		if (flipFrog) 
			Flip();

		if(rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
