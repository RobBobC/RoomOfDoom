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
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime)/* + collisionAvoidance(direction)*/;

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

    //private Vector2 collisionAvoidance(Vector3 direction) 
    //{
    //    Vector3 ahead = this.transform.position + moveSpeed * 2f;
    //    Vector3 ahead2 = this.transform.position + moveSpeed * 2f * 0.5;
 
    //    var mostThreatening = findMostThreateningObstacle();
    //    var avoidance = new Vector3(0, 0, 0);
 
    //    if (mostThreatening != null) {
    //        avoidance.x = ahead.x - mostThreatening.transform.position.x;
    //        avoidance.y = ahead.y - mostThreatening.transform.position.y;
 
    //        avoidance.normalize();
    //        avoidance.scaleBy(MAX_AVOID_FORCE);
    //    } 
    //    else 
    //    {
    //        avoidance.scaleBy(0); // nullify the avoidance force
    //    }
 
    //    return avoidance;
    //}
 
    //private GameObject findMostThreateningObstacle() 
    //{
    //    var mostThreatening :Obstacle = null;
     
    //    for (var i:int = 0; i < Game.instance.obstacles.length; i++) {
    //        var obstacle :Obstacle = Game.instance.obstacles[i];
    //        var collision :Boolean = lineIntersecsCircle(ahead, ahead2, obstacle);
     
    //        // "position" is the character's current position
    //        if (collision && (mostThreatening == null || distance(position, obstacle) < distance(position, mostThreatening))) {
    //            mostThreatening = obstacle;
    //        }
    //    }
    //    return mostThreatening;
    //}
}
