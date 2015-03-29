using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float moveSpeed = 1.0f;

	Animator animator;
	int currentAnimation;

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
		currentAnimation = (int)Animation.IDLE_UP;
		UpdateAnimationState (currentAnimation);
	}
	
	void Update()
	{
		if (Input.GetKey ("a") && Input.GetKey ("w")) {
			UpdateAnimationState ((int)Animation.UP_LEFT);
			transform.Translate (new Vector3 (-1, 1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("a") && Input.GetKey ("s")) {
			UpdateAnimationState ((int)Animation.DOWN_LEFT);
			transform.Translate (new Vector3 (-1, -1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d") && Input.GetKey ("w")) {
			UpdateAnimationState ((int)Animation.UP_RIGHT);
			transform.Translate (new Vector3 (1, 1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d") && Input.GetKey ("s")) {
			UpdateAnimationState ((int)Animation.DOWN_RIGHT);
			transform.Translate (new Vector3 (1, -1, 0) * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("w")) { 
			UpdateAnimationState ((int)Animation.UP);
			transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("s")) {
			UpdateAnimationState ((int)Animation.DOWN);
			transform.Translate (Vector3.down * moveSpeed * Time.deltaTime);	
		} else if (Input.GetKey ("a")) {
			UpdateAnimationState ((int)Animation.LEFT);
			transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d")) {
			UpdateAnimationState ((int)Animation.RIGHT);
			transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
		}
	}
	
	void UpdateAnimationState(int curAnimState)
	{
		if (currentAnimation == (int)Animation.IDLE_UP)
			return;
		
		switch (curAnimState)
		{
			case (int)Animation.IDLE_UP:
				animator.SetInteger("animationState", (int)Animation.IDLE_UP);
				break;
			case (int)Animation.UP:
				animator.SetInteger("animationState", (int)Animation.UP);
				break;
			case (int)Animation.DOWN:
				animator.SetInteger("animationState", (int)Animation.DOWN);
				break;
			case (int)Animation.LEFT:
				animator.SetInteger("animationState", (int)Animation.LEFT);
				break;
			case (int)Animation.RIGHT:
				animator.SetInteger("animationState", (int)Animation.RIGHT);
				break;
		}
		
		currentAnimation = curAnimState;
	}
}
