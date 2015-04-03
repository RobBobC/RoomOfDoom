using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float moveSpeed = 1.0f;

	private Animator animator;

	private enum Animation {
		IDLE,
		WALK
	};

	Animation currentAnimation;
	bool blocked = false;

	void Start()
	{
		animator = this.GetComponent<Animator>();
		currentAnimation = Animation.IDLE;
		UpdateAnimationState (currentAnimation);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Barrier" || collider.tag == "Enemy")
		{
			blocked = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		blocked = false;
	}
	
	void Update()
	{
		Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		Vector3 moveDirection = moveToward - this.transform.position;
		moveDirection.z = 0; 
		moveDirection.Normalize();
		
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler( 0, 0, targetAngle - 90 ), 5 * Time.deltaTime );
		if(!blocked)
		{
			if (Input.GetKey ("a") && Input.GetKey ("w")) 
			{
				UpdateAnimationState (Animation.WALK);
				transform.Translate (new Vector3 (-1, 1, 0) * moveSpeed * Time.deltaTime);
			} 
			else if (Input.GetKey ("a") && Input.GetKey ("s")) 
			{
				UpdateAnimationState (Animation.WALK);
				transform.Translate (new Vector3 (-1, -1, 0) * moveSpeed * Time.deltaTime);
			} 
			else if (Input.GetKey ("d") && Input.GetKey ("w")) 
			{
				UpdateAnimationState (Animation.WALK);
				transform.Translate (new Vector3 (1, 1, 0) * moveSpeed * Time.deltaTime);
			} 
			else if (Input.GetKey ("d") && Input.GetKey ("s")) 
			{
				UpdateAnimationState (Animation.WALK);
				transform.Translate (new Vector3 (1, -1, 0) * moveSpeed * Time.deltaTime);
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
}
