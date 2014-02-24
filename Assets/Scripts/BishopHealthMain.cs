using UnityEngine;
using System.Collections;

public class BishopHealthMain : MonoBehaviour 
{
	public float BishopHealth = 100f;
	public float BishopCurrentHealth;

	public float noDamagePeriod = 2f;
	private float lastHitTime;

	public float noPikeDamagePeriod = 0.5f;
	private float lastPikeHitTime;

	private SpriteRenderer bishopsRenderer;

	public float FadeTime = 5f;
	public Texture FadeTexture;
	private float AlphaFadeValue = 0f;
	private bool collided;

	private BishopControls bishopControls;
	private BoxCollider2D bishopsCollider;
	private CircleCollider2D bishopsCircleCollider;

	// Use this for initialization
	void Start () 
	{
		BishopCurrentHealth = BishopHealth;
		bishopsRenderer = GetComponent<SpriteRenderer> ();

		bishopControls = GetComponent<BishopControls>();
		bishopsCollider = GetComponent<BoxCollider2D> ();
		bishopsCircleCollider = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Frog")
		{
			if(Time.time > lastHitTime + noDamagePeriod)
			{
				if(BishopCurrentHealth>10f)
				{
					lastHitTime = Time.time; 
					BishopCurrentHealth-= 10f;
					StartCoroutine(PlayerFlashing());
				}
				else
				{
					BishopCurrentHealth -=10f;
					LevelReset ();
				}
			}
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Pike")
		{	
				if(BishopCurrentHealth > 1f)
				{
			       BishopCurrentHealth -= 0.1f;
				}
				else
				{
					BishopCurrentHealth -=1f;
					LevelReset ();
				}
		}
	}

	void LevelReset()
	{
		collided = true;
		bishopsCollider.enabled = false;
		bishopsCircleCollider.enabled = false;
		rigidbody2D.isKinematic = true;
		bishopControls.enabled = false;
		StartCoroutine(restartLevel());
	}

	void OnGUI()
	{
		if (collided) 
		{
			AlphaFadeValue = Mathf.Clamp01 (AlphaFadeValue + (Time.deltaTime / FadeTime));
			GUI.color = new Color (0, 0, 0, AlphaFadeValue);
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), FadeTexture);
		}
	}

	IEnumerator restartLevel()
	{
		yield return new WaitForSeconds(5f);
		Application.LoadLevel (Application.loadedLevelName);
	}

	IEnumerator PlayerFlashing()
	{
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		bishopsRenderer.enabled  = true;
	}


}
