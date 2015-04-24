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

    bool avoidingCollision = false;
    bool playerInRange;
    float meleeAttackTimer;
    float timeToAvoidCollision = .1f;
    PlayerController playerController;
    SpawnController spawnController;

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
        spawnController = GameObject.FindGameObjectWithTag("SpawnController").GetComponent<SpawnController>();
	}
    
	protected void Update()
    {
        direction = player.transform.position - transform.position;
        //Debug.Log(Physics2D.Raycast(this.transform.position + this.transform.localScale, direction, 2f).collider.tag);
        if (Physics2D.Raycast(this.transform.position, direction, 10f).collider.tag == "Obstacle")
        {
            avoidingCollision = true;
            Quaternion angleFacing = transform.rotation;
            if (this.name == "rat(Clone)" || this.name == "demon(Clone)")
            {
                transform.rotation = new Quaternion(angleFacing.x, angleFacing.y, angleFacing.z + 90, angleFacing.w);
            }
        }

        if (avoidingCollision)
        {
            timeToAvoidCollision -= Time.deltaTime;
            if (timeToAvoidCollision <= 0)
            {
                avoidingCollision = false;
                timeToAvoidCollision = .1f;
            }
            else
            {
                Vector3 target = new Vector3(transform.position.x + .1f, transform.position.y + .1f, transform.position.z);
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

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
        spawnController.enemyCount--;
        ScoreController.score += scoreValue;
    }
}
