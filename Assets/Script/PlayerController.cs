using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 1.0f;
	public int health = 3;
	public float shotSpeed = 1000;

	private enum Animation {
		IDLE,
		WALK
	};

	private Animator animator;
	private bool invincible;
	private bool dead;
	private float invinceDuration;
	private CrosshairController crosshair;
	private GameObject launchBox;
	private Animation currentAnimation;
	private WeaponController weapon;

	void Start()
	{
		weapon = GetComponent<WeaponController>();
		crosshair = GameObject.FindGameObjectWithTag ("Crosshair").GetComponent<CrosshairController>();
		animator = this.GetComponent<Animator>();
		currentAnimation = Animation.IDLE;
		UpdateAnimationState (currentAnimation);
		invincible = false;
		dead = false;
		invinceDuration = 1.0f;
		launchBox = GameObject.FindGameObjectWithTag ("LaunchBox");
	}
	
	void Update()
	{
		if(health <=0)
		{
			Time.timeScale = 0;
		}
		if (Input.GetKey ("r") && Time.timeScale == 0)
		{
			Application.LoadLevel(0);
			Time.timeScale = 1;
		}
		if(launchBox.GetComponent<PolygonCollider2D>().enabled == true && launchBox.GetComponent<PolygonCollider2D>().enabled == true)
		{
			launchBox.GetComponent<PolygonCollider2D>().enabled = false;
		}
		if(invincible)
		{
			if(invinceDuration < 0)
			{
				invincible = false;
				invinceDuration = 1.0f;
			}
			else
				invinceDuration -= Time.deltaTime;
		}

		if (Input.GetKey ("a") && Input.GetKey ("w"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 (-(1/Mathf.Sqrt(2)), (1/Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("a") && Input.GetKey ("s"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 (-(1/Mathf.Sqrt(2)), -(1/Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("d") && Input.GetKey ("w"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 ((1/Mathf.Sqrt(2)), (1/Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("d") && Input.GetKey ("s"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 ((1/Mathf.Sqrt(2)), -(1/Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("w"))
		{ 
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("s"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.down * moveSpeed * Time.deltaTime);	
		}
		else if (Input.GetKey ("a"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("d"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
		}
		else
		{
			UpdateAnimationState (Animation.IDLE);
		}

		Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector3 moveDirection = moveToward - this.transform.position;
		moveDirection.z = 0; 
		moveDirection.Normalize();

		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler( 0, 0, targetAngle - 90 ), 5 * Time.deltaTime);

		if(Input.GetMouseButtonDown(0))
		{
			if(weapon.type == WeaponController.attackType.ranged)
			{
				GameObject shot = Instantiate(weapon.weapon, launchBox.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
				shot.rigidbody2D.AddForce(moveDirection * shotSpeed);
			}
			else
			{
				launchBox.GetComponent<PolygonCollider2D>().enabled = true;
			}
		}
	}
	
	void UpdateAnimationState(Animation curAnimState)
	{
		if (currentAnimation == curAnimState)
			return;
		
		switch (curAnimState)
		{
			case Animation.IDLE:
				animator.SetInteger("animationState", 0);
				break;
			case Animation.WALK:
				animator.SetInteger("animationState", 1);
				break;
		}
		
		currentAnimation = curAnimState;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(!invincible)
		{
			switch(other.collider.tag)
			{
				case "Rat":
					health -= 1;
					invincible = true;
					break;
			}
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if(!invincible)
		{
			switch(other.collider.tag)
			{
				case "Rat":
					health -= 1;
				    invincible = true;
					break;
			}		
		}
	}

}
