using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
    public int health = 100;
    public int attackDamage = 10;
    public int scoreValue = 20;
	public float moveSpeed;
    public float spawnSoundVolume = 1.0f;
    public float dieSoundVolume = 1.0f;
    public float timeBetweenAttacks = 1.5f;
	public AudioClip spawnSound;
	public AudioClip dieSound;

	protected GameObject player;
	protected Animator animator;
	protected Vector3 direction;
    protected PolygonCollider2D polygonCollider2D;

    bool playerInRange;
    float meleeAttackTimer;
    PlayerController playerController;

	protected enum attackType {
		melee,
		ranged
	};
    
	protected void Start()
	{
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
		AudioSource.PlayClipAtPoint (spawnSound, new Vector3 (0, 0, 0), spawnSoundVolume);
	}
    
	protected void Update()
	{
        meleeAttackTimer += Time.deltaTime;

        if (meleeAttackTimer >= timeBetweenAttacks && playerInRange && health > 0)
            Attack();

		if(rigidbody2D.isKinematic)
		{
			rigidbody2D.isKinematic = false;
		}
	}

	protected void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject == player)
            playerInRange = true;

		if (other.collider.tag == "Bullet")
		{
			gameObject.rigidbody2D.isKinematic = true;
			health -= other.collider.gameObject.GetComponent<BulletController>().damagePoints;

			if(health <= 0)
			{
				AudioSource.PlayClipAtPoint (dieSound, new Vector3 (0, 0, 0),dieSoundVolume);
                Death();
			}

			Destroy (other.gameObject);
		}
	}

    protected void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
            playerInRange = false;
    }

	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "LaunchBox")
		{
			health -= other.gameObject.GetComponentInParent<WeaponController>().weapon.GetComponent<MeleeController>().damage;

			if(health <= 0)
			{
                Death();
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
                Death();
			}
		}
	}
    
	protected void GrantTheSweetReleaseOfDeath()
	{
		Destroy (gameObject);
	}

    void Attack()
    {
        meleeAttackTimer = 0f;

        if (playerController.health > 0)
            playerController.Damage(attackDamage);
    }

    protected bool IsDead
    {
        get { return animator.GetBool("isDead"); }
    }

    void Death()
    {
        animator.SetBool("isDead", true);
        gameObject.collider2D.enabled = false;
        ScoreController.score += scoreValue;
    }
}
