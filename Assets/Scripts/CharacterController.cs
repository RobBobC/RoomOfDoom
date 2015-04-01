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
	CrosshairController crosshair;
	
	/*void AimAt ()
	{
		RaycastHit rayCastHit;
		Vector3 vecLookToward;
		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (mouseRay, out rayCastHit, 150.0f)) {
			vecLookToward = rayCastHit.point;
		} else {
			vecLookToward = mouseRay.origin + mouseRay.direction * 50.0f;
		}
		transform.LookAt (vecLookToward);
	}

	Animation ChangeAnimation (Animation anim)
	{
		if(anim == Animation.UP)
		else if(direction == Direction.South)
		else if(dir == Direction.East)
		else if(dir == Direction.West)

		return dir;     
	}*/

	void Start()
	{
		crosshair = GameObject.FindGameObjectWithTag ("Crosshair").GetComponent<CrosshairController>();
		animator = this.GetComponent<Animator>();
		currentAnimation = Animation.IDLE;
		UpdateAnimationState (currentAnimation);
	}
	
	void Update()
	{
		if (Input.GetKey ("a") && Input.GetKey ("w")) {
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 (-1, 1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("a") && Input.GetKey ("s")) {
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 (-1, -1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d") && Input.GetKey ("w")) {
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 (1, 1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d") && Input.GetKey ("s")) {
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 (1, -1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("w")) { 
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("s")) {
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.down * moveSpeed * Time.deltaTime);	
		} else if (Input.GetKey ("a")) {
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d")) {
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
		}
		else {
			UpdateAnimationState (Animation.IDLE);
		}

		Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		// 4
		Vector3 moveDirection = moveToward - this.transform.position;
		moveDirection.z = 0; 
		moveDirection.Normalize();
		
		
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
			                 Quaternion.Euler( 0, 0, targetAngle - 90 ), 
			                 5 * Time.deltaTime );
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
