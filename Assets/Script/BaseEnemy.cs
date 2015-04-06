using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	public float moveSpeed;
	public int health;

	protected GameObject player;
	protected Animator animator;
	protected Vector3 direction;

	protected enum attackType {
		melee,
		ranged
	};

	protected bool IsDead //property to retrieve the isDead flag
	{
		get { return animator.GetBool("isDead"); }
	}

	protected void Start()
	{
		animator = this.GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	protected void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Bullet")
		{
			health -= other.collider.gameObject.GetComponent<BulletController>().damagePoints;
			if(health <= 0)
			{
				animator.SetBool("isDead", true);
				gameObject.collider2D.enabled = false;
			}
			Destroy (other.gameObject);
		}
	}

	protected void GrantTheSweetReleaseOfDeath() //animation event
	{
		Destroy (gameObject);
	}
}
