﻿using UnityEngine;
using System.Collections;

public class FrogBehaviour : MonoBehaviour 
{
	public float moveForce = 20f;
	public float maxSpeed = 4f;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce(Vector2.right * moveForce);

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
	}
}
