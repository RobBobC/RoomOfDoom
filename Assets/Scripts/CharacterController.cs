using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float moveSpeed = 1.0f;
	
	//private Animator animator;
	
	private const int STATE_IDLE = 0;
	private const int STATE_UP = 1;
	private const int STATE_RIGHT = 2;
	private const int STATE_DOWN = 3;
	private const int STATE_LEFT = 4;
	
	private bool isIdle = true;
	
	private int currentAnimationState = STATE_IDLE;
	
	void Start()
	{
		//animator = this.GetComponent<Animator>();
	}
	
	void Update()
	{
		if (Input.GetKey ("a") && Input.GetKey ("w")) {
						transform.Translate (new Vector3 (-1, 1, 0) * moveSpeed * Time.deltaTime);
				}
		else if (Input.GetKey ("a") && Input.GetKey ("s")) {
			transform.Translate (new Vector3 (-1, -1, 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("d") && Input.GetKey ("w")) {
			transform.Translate (new Vector3 (1, 1, 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("d") && Input.GetKey ("s")) {
			transform.Translate (new Vector3 (1, -1, 0) * moveSpeed * Time.deltaTime);
		}
			else if (Input.GetKey ("w")) { 
					UpdateState (STATE_UP);
					transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
			} else if (Input.GetKey ("d")) {
					UpdateState (STATE_RIGHT);
					transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
			} else if (Input.GetKey ("s")) {
					UpdateState (STATE_DOWN);
					transform.Translate (Vector3.down * moveSpeed * Time.deltaTime);	
			} else if (Input.GetKey ("a")) {
					UpdateState (STATE_LEFT);
					transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
			}
			else if (isIdle)
			{
				//UpdateState(STATE_IDLE);
			}
	}
	
	void UpdateState(int state)
	{
		/*if (currentAnimationState == state)
			return;
		
		switch (state)
		{
		case STATE_IDLE:
			animator.SetInteger("PlayerState", STATE_IDLE);
			break;
		case STATE_UP:
			animator.SetInteger("PlayerState", STATE_UP);
			break;
		case STATE_RIGHT:
			animator.SetInteger("PlayerState", STATE_RIGHT);
			break;
		case STATE_DOWN:
			animator.SetInteger("PlayerState", STATE_DOWN);
			break;
		case STATE_LEFT:
			animator.SetInteger("PlayerState", STATE_LEFT);
			break;
		}
		
		currentAnimationState = state;*/
	}
}
