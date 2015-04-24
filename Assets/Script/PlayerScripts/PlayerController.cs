using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public int health = 100;
	public float shotSpeed = 1000;
	public float fireRate;
    public float nextFire;
    public float flashSpeed = 5f;
    public Image damageImage;
    public Slider healthSlider;
    public Color flashColor;

    [HideInInspector]
    public bool dead;
    [HideInInspector]
    public List<GameObject> inventory;
    
    int weaponReward;
	float invinceDuration;
    bool damaged;
    bool invincible;
	GameObject launchBox;
	WeaponController weapon;
	ChestController chestController;
    AudioSource playerAudio;
    BaseEnemy baseEnemy;
    
    void Start()
    {
        weaponReward = 0;
        invinceDuration = 1.0f;
        invincible = false;
        dead = false;
        inventory = new List<GameObject>();
        launchBox = GameObject.FindGameObjectWithTag("LaunchBox");
        weapon = GetComponent<WeaponController>();
        inventory.Add(weapon.weapon);
        playerAudio = GetComponent<AudioSource>();
    }

	void Update()
	{
        Debug.Log(inventory.Count);
        if (health <= 0 && !dead)
        {
            Death();
        }

        if (damaged)
            damageImage.color = flashColor;
        else
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        
        damaged = false;

		if(launchBox.GetComponent<PolygonCollider2D>().enabled == true)
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

		Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector3 moveDirection = moveToward - transform.position;
		moveDirection.z = 0; 
		moveDirection.Normalize();

		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle - 90), 5 * Time.deltaTime);

        if (Input.GetButton("Fire1") && Time.time > nextFire || Input.GetButtonDown("Fire1"))
		{
			nextFire = Time.time + fireRate;

			if(weapon.type == WeaponController.attackType.ranged)
			{
                if (weapon.weapon.name == "bullet")
                {
				    GameObject shot = Instantiate(weapon.weapon, launchBox.transform.position, Quaternion.Euler(0,0, targetAngle)) as GameObject;
				    shot.rigidbody2D.AddForce(moveDirection * weapon.weapon.GetComponent<BulletController>().shotSpeed);
                }
                else
                {
				    GameObject shot = Instantiate(weapon.weapon, launchBox.transform.position, Quaternion.Euler(0,0, targetAngle - 45)) as GameObject;
                    shot.rigidbody2D.AddForce(moveDirection * weapon.weapon.GetComponent<BulletController>().shotSpeed);
                }
			}
			else
			{
				launchBox.GetComponent<PolygonCollider2D>().enabled = true;
			}
		}

        // Hammer
		if (Input.GetKey(KeyCode.Alpha1))
		{
            weapon.weapon = inventory[0];
			weapon.type = inventory[0].GetComponent<MeleeController>() != null ? WeaponController.attackType.melee : WeaponController.attackType.ranged;
        }
        // Throwing Axes
		else if(Input.GetKey(KeyCode.Alpha2))
		{
			if(inventory.Count >= 2)
			{
				weapon.weapon = inventory[1];
				weapon.type = inventory[1].GetComponent<MeleeController>() != null ? WeaponController.attackType.melee : WeaponController.attackType.ranged;
            }
		}
        // Crossbows
		else if(Input.GetKey(KeyCode.Alpha3))
		{
            if(inventory.Count >= 3)
			{
				weapon.weapon = inventory[2];
				weapon.type = inventory[2].GetComponent<MeleeController>() != null ? WeaponController.attackType.melee : WeaponController.attackType.ranged;
            }
		}
        // Dualies
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            if (inventory.Count >= 4)
            {
                weapon.weapon = inventory[3];
                weapon.type = inventory[3].GetComponent<MeleeController>() != null ? WeaponController.attackType.melee : WeaponController.attackType.ranged;
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!invincible)
		{
			switch(other.tag)
			{
				case "EnemyBullet":
                    Damage(other.gameObject.GetComponent<BulletController>().damagePoints);
					invincible = true;
					Destroy(other.gameObject);
					break;
                case "Lava":
                    Damage(1);
				    invincible = true;
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
        else if (other.gameObject.tag == "Lava" && !invincible)
		{
            Damage(1);
			invincible = true;
		}
	}

    public void Damage(int amount)
    {
        if (!invincible)
            invincible = true;

        damaged = true;
        health -= amount;
        healthSlider.value = health;
        playerAudio.Play();
    }

    void Death()
    {
        dead = true;
        Time.timeScale = 0;
    }
}
