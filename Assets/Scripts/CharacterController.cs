using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float moveSpeed = 1.0f;

	Animator animator;

	enum Animation {
		IDLE_UP,
		IDLE_DOWN,
		IDLE_LEFT,
		IDLE_RIGHT,
		UP,
		DOWN,
		LEFT,
		RIGHT,
		UP_LEFT,
		UP_RIGHT,
		DOWN_LEFT,
		DOWN_RIGHT
	}

	Animation currentAnimation;
	
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
		animator = this.GetComponent<Animator>();
		currentAnimation = Animation.IDLE_UP;
		UpdateAnimationState (currentAnimation);
	}
	
	void Update()
	{
		if (Input.GetKey ("a") && Input.GetKey ("w")) {
			UpdateAnimationState (Animation.UP_LEFT);
			transform.Translate (new Vector3 (-1, 1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("a") && Input.GetKey ("s")) {
			UpdateAnimationState (Animation.DOWN_LEFT);
			transform.Translate (new Vector3 (-1, -1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d") && Input.GetKey ("w")) {
			UpdateAnimationState (Animation.UP_RIGHT);
			transform.Translate (new Vector3 (1, 1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d") && Input.GetKey ("s")) {
			UpdateAnimationState (Animation.DOWN_RIGHT);
			transform.Translate (new Vector3 (1, -1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("w")) { 
			UpdateAnimationState (Animation.UP);
			transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("s")) {
			UpdateAnimationState (Animation.DOWN);
			transform.Translate (Vector3.down * moveSpeed * Time.deltaTime);	
		} else if (Input.GetKey ("a")) {
			UpdateAnimationState (Animation.LEFT);
			transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d")) {
			UpdateAnimationState (Animation.RIGHT);
			transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
		}
	}
	
	void UpdateAnimationState(Animation curAnimState)
	{
		if (currentAnimation == curAnimState)
			return;
		
		switch (curAnimState)
		{
			case Animation.IDLE_UP:
				animator.SetInteger("animationState", (int)Animation.IDLE_UP);
				break;
			case Animation.UP:
				animator.SetInteger("animationState", (int)Animation.UP);
				break;
			case Animation.DOWN:
				animator.SetInteger("animationState", (int)Animation.DOWN);
				break;
			case Animation.LEFT:
				animator.SetInteger("animationState", (int)Animation.LEFT);
				break;
			case Animation.RIGHT:
				animator.SetInteger("animationState", (int)Animation.RIGHT);
				break;
		}
		
		currentAnimation = curAnimState;
	}
}
