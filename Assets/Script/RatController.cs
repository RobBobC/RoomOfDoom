using UnityEngine;
using System.Collections;

public class RatController : BaseEnemy {
	private attackType attack;
	private GameObject player;
	private float lookAtAngle;
	private Animator animator;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = this.GetComponent<Animator> ();
		health = 2;
	}

	void Update ()
	{
		if (animator.GetBool ("isDead"))
			return;
		if(rigidbody2D.isKinematic)
			rigidbody2D.isKinematic = false;
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		direction = player.transform.position - transform.position;
		lookAtAngle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (lookAtAngle - 90, Vector3.forward);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Bullet")
		{
			health--;
			if(health == 0)
			{
				animator.SetBool("isDead", true);
				gameObject.collider2D.enabled = false;

			}
			rigidbody2D.isKinematic = true;
			Destroy (other.gameObject);
		}
	}

	void GrantTheSweetReleaseOfDeath() //animation event
	{
		Destroy (gameObject);
	}
}
