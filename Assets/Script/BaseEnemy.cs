using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	public float moveSpeed;
	public int health;
	public AudioClip spawnSound;
	public AudioClip dieSound;
	public float spawnSoundVolume = 1.0f;
	public float dieSoundVolume = 1.0f;
	
	private AudioSource audioSource;
	private SpawnController spawnController;
	protected GameObject player;
	protected Animator animator;
	protected Vector3 direction;

	protected enum attackType {
		melee,
		ranged
	};

	protected bool IsDead
	{
		get { return animator.GetBool("isDead"); }
	}

	// Use this for initialization
	protected void Start()
	{
		animator = gameObject.GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		AudioSource.PlayClipAtPoint (spawnSound, new Vector3 (0, 0, 0), spawnSoundVolume);
		spawnController = GameObject.FindGameObjectWithTag ("SpawnController").GetComponent<SpawnController> ();
	}

	// Update is called once per frame
	protected void Update()
	{
		if(gameObject.rigidbody2D.isKinematic)
		{
			gameObject.rigidbody2D.isKinematic = false;
		}
	}

	protected void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Bullet")
		{
			gameObject.rigidbody2D.isKinematic = true;
			health -= other.collider.gameObject.GetComponent<BulletController>().damagePoints;

			if(health <= 0)
			{
				animator.SetBool("isDead", true);
				AudioSource.PlayClipAtPoint (dieSound, new Vector3 (0, 0, 0),dieSoundVolume);
				gameObject.collider2D.enabled = false;
				spawnController.enemyCount--;
			}

			Destroy (other.gameObject);
		}
	}

	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "LaunchBox")
		{
			health -= other.gameObject.GetComponentInParent<WeaponController>().weapon.GetComponent<MeleeController>().damage;

			if(health <= 0)
			{
				animator.SetBool("isDead", true);
				gameObject.collider2D.enabled = false;
				spawnController.enemyCount--;
			}
		}
	}

	protected void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "LaunchBox")
		{
			health -= other.gameObject.GetComponentInParent<WeaponController>().weapon.GetComponent<MeleeController>().damage;

			if(health <= 0)
			{
				animator.SetBool("isDead", true);
				gameObject.collider2D.enabled = false;
				spawnController.enemyCount--;
			}
		}
	}

	protected void GrantTheSweetReleaseOfDeath()
	{
		Destroy (gameObject);
	}
}
