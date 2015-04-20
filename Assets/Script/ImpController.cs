using UnityEngine;
using System.Collections;

public class ImpController : BaseEnemy {
	public GameObject fireball;
	public int shotSpeed = 1000;
	public GameObject launchBox;
	public int launchBoxRotatationSpeed;

	private bool coolDown;
	private float coolDownDuration;

	void FireBall()
	{
		if(direction.magnitude < 25)
		{
			direction.Normalize ();
			GameObject shot = Instantiate(fireball, launchBox.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
			shot.rigidbody2D.AddForce(direction * shotSpeed);
			coolDown = true;
		}
	}

	// Use this for initialization
	void Start()
	{
		base.Start();
		coolDownDuration = 1.5f;
		coolDown = false;
	}

	// Update is called once per frame
	void Update()
	{
		if(IsDead)
		{
			return;
		}

		base.Update();

		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		direction = player.transform.position - transform.position;

		launchBox.transform.RotateAround (transform.position, Vector3.forward, launchBoxRotatationSpeed * Time.deltaTime);

		if(coolDown)
		{
			if(coolDownDuration < 0)
			{
				coolDown = false;
				coolDownDuration = 1.0f;
			}
			else
				coolDownDuration -= Time.deltaTime;
		}
		else
		{
			FireBall();
		}
	}
}
