using UnityEngine;
using System.Collections;

public class BishopControls : MonoBehaviour 
	
{
	public float JumpForce = 300f;
	public float MaxSpeed = 3f;
	public float MoveForce = 20f;

	private bool facingRight = true;
	private bool grounded = false;
	private bool jump = false;

	private Transform groundCheck;

	//Used for initialization.
	void Start () 
	{
		groundCheck = transform.Find("groundCheck");
	}

	//Update() is called every frame. Used for regular updates such as moving non physics objects.
	void Update()
	{
		//Creates a "Line" from the player's transform, to the groundCheck gameObjects transform. grounded boolean becomes true if this line is intersected by the ground layer.
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		//If the player is "grounded" and presses the Unity preset Jump input (spacebar or Y on controller) the jump boolean is set to true and so the If statement...
		// In the FixedUpdate method runs.
		if (Input.GetButtonDown ("Jump") && grounded)
			jump = true;
	
	}
	// FixedUpdate is called every physics step. FixedUpdate intervals are consistent. Used for regular updates such as : adjusting physics of rigidbody objects.
	void FixedUpdate () 
	{
		//Controls for players movement.
		//Sets h to +/- 1 depending on which way the player presses on either a controller joystick or w, a, left/right arrow, which are preset into Unity under "Horizontal" Input.
		float h = Input.GetAxis ("Horizontal");

		//If his velocity in a certain direction is less than his max speed, he can move that way.
		if (h * rigidbody2D.velocity.x < MaxSpeed) 
			rigidbody2D.AddForce (Vector2.right * h * MoveForce);

		//If his velocity is greater than his maxspeed, his velocity is set back to his max speed.
		if (Mathf.Abs(rigidbody2D.velocity.x) > MaxSpeed)
			rigidbody2D.velocity = new Vector2 (Mathf.Sign(rigidbody2D.velocity.x) * MaxSpeed, rigidbody2D.velocity.y);

		if (h == 0 && grounded)
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);

		if (jump) 
		{
			//Adds a new vector to the players rigidbody with 0 force in the X axis and jumpforce in the Y axis. Jump is reset to false and will remain this way until...
			//the player is regrounded.
			rigidbody2D.AddForce(new Vector2(0f, JumpForce));
			jump = false;
		}

		//if the input is moving the player right and the player is facing left flip him.
		if (h > 0 && !facingRight) 
			Flip ();

		// Otherwise if the input is moving the player left and the player is facing right flip him.
		else if (h < 0 && facingRight)
			Flip ();
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1 to flip him.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
}
