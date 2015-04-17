using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 1.0f;
	public int health = 80;
	public float shotSpeed = 1000;
	public float fireRate;
    public RuntimeAnimatorController[] animators;

	private enum Animation {
		IDLE,
		WALK
	};

	private Animator animator;
	private bool invincible;
	private bool dead;
	private float invinceDuration;
	private float nextFire;
	private GameObject launchBox;
	private Animation currentAnimation;
	private WeaponController weapon;
	private List<GameObject> inventory;
	private ChestController chestController;
	private int inventoryIndex = 0;
	private int weaponReward = 0;

	// Use this for initialization
	void Start()
	{
		inventory = new List<GameObject> ();
		launchBox = GameObject.FindGameObjectWithTag ("LaunchBox");

		weapon = GetComponent<WeaponController>();
		inventory.Add (weapon.weapon);
		animator = gameObject.GetComponent<Animator>();
		currentAnimation = Animation.IDLE;
		UpdateAnimationState (currentAnimation);

		invincible = false;
		dead = false;

		invinceDuration = 1.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if(health <= 0)
		{
			dead = true;
			Time.timeScale = 0;

			if (dead)
			{
				Application.LoadLevel(3);
				Time.timeScale = 1;
			}
		}

		if(launchBox.GetComponent<PolygonCollider2D>().enabled == true && launchBox.GetComponent<PolygonCollider2D>().enabled == true)
		{
			launchBox.GetComponent<PolygonCollider2D>().enabled = false;
		}

		if(invincible)
		{
			if(invinceDuration < 0)
			{
				invincible = false;
				invinceDuration = 1.0f;
			}
			else
				invinceDuration -= Time.deltaTime;
		}

		if (Input.GetKey ("a") && Input.GetKey ("w"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 (-(1/Mathf.Sqrt(2)), (1/Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("a") && Input.GetKey ("s"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 (-(1/Mathf.Sqrt(2)), -(1/Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("d") && Input.GetKey ("w"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (new Vector3 ((1/Mathf.Sqrt(2)), (1/Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("d") && Input.GetKey ("s"))
		{
			transform.Translate (new Vector3 ((1/Mathf.Sqrt(2)), -(1/Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
			UpdateAnimationState (Animation.WALK);
		}
		else if (Input.GetKey ("w"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("s"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.down * moveSpeed * Time.deltaTime);	
		}
		else if (Input.GetKey ("a"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey ("d"))
		{
			UpdateAnimationState (Animation.WALK);
			transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
		}
		else
		{
			UpdateAnimationState (Animation.IDLE);
		}

		Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector3 moveDirection = moveToward - transform.position;
		moveDirection.z = 0; 
		moveDirection.Normalize();

		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle - 90), 5 * Time.deltaTime);

		if (Input.GetButton ("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;

			if(weapon.type == WeaponController.attackType.ranged)
			{
				GameObject shot = Instantiate(weapon.weapon, launchBox.transform.position, Quaternion.Euler(0,0, targetAngle - 45)) as GameObject;
				shot.rigidbody2D.AddForce(moveDirection * shotSpeed);
			}
			else
			{
				launchBox.GetComponent<PolygonCollider2D>().enabled = true;
			}
		}

        // Melee weapon
		if (Input.GetKey(KeyCode.Alpha1))
		{
            animator.runtimeAnimatorController = animators[0];
            weapon.weapon = inventory[0];
			weapon.type = inventory[0].GetComponent<MeleeController>() != null ? WeaponController.attackType.melee : WeaponController.attackType.ranged;
		}

        // Ranged weapon 1
		else if(Input.GetKey(KeyCode.Alpha2))
		{
            animator.runtimeAnimatorController = animators[1];
			if(inventory.Count >= 2)
			{
				weapon.weapon = inventory[1];
				weapon.type = inventory[1].GetComponent<MeleeController>() != null ? WeaponController.attackType.melee : WeaponController.attackType.ranged;
			}
		}

        // Ranged weapon 2
		else if(Input.GetKey(KeyCode.Alpha3))
		{
            //animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/Player/RangedPlayer.controller", typeof(RuntimeAnimatorController));
            animator.runtimeAnimatorController = animators[1];
            if(inventory.Count >= 3)
			{
				weapon.weapon = inventory[2];
				weapon.type = inventory[2].GetComponent<MeleeController>() != null ? WeaponController.attackType.melee : WeaponController.attackType.ranged;
			}
		}
	}

	void LateUpdate ()
	{
		if (rigidbody2D.velocity.magnitude > 0)
		{
			rigidbody2D.velocity = new Vector2(0, 0);
			rigidbody2D.angularVelocity = 0;
		}
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

	void OnCollisionEnter2D(Collision2D other)
	{
		if(!invincible)
		{
			switch(other.collider.tag)
			{
				case "Enemy":
					health -= 1;
					invincible = true;
					break;
			}
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if(!invincible)
		{
			switch(other.collider.tag)
			{
				case "Enemy":
					health -= 1;
				    invincible = true;
					break;
			}		
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!invincible)
		{
			switch(other.tag)
			{
				case "EnemyBullet":
					health -= other.gameObject.GetComponent<BulletController>().damagePoints;
					invincible = true;
					Destroy(other.gameObject);
					break;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Chest")
		{
			chestController = other.gameObject.GetComponent<ChestController>();
			if (Input.GetKey ("e") && chestController.collectable)
			{
				chestController.OpenChest();
				chestController.collectable = false;

				switch (weaponReward)
				{
					case 0:
						inventory.Add(other.gameObject.GetComponent<ChestController>().weaponRewardOne);
						weaponReward++;
						break;
					case 1:
						inventory.Add(other.gameObject.GetComponent<ChestController>().weaponRewardTwo);
						weaponReward++;
						break;
					default:
						break;
				}
			}
		}
	}
	
}
