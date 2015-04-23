using UnityEngine;
using System.Collections;

public class SkeletonController : BaseEnemy {
	public int shotSpeed = 1000;
    public int launchBoxRotatationSpeed;
	public GameObject launchBox;
    public GameObject bone;

	bool coolDown;
	float coolDownDuration;

	void ThrowBone()
	{
		if(direction.magnitude < 25)
		{
			direction.Normalize ();
			GameObject shot = Instantiate(bone, launchBox.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
			shot.rigidbody2D.AddForce(direction * shotSpeed);
			coolDown = true;
		}
	}
    
	void Start()
	{
		base.Start();
		coolDownDuration = 1.5f;
		coolDown = false;
	}

	void Update()
	{
		if(IsDead)
		{
			return;
		}

		base.Update();

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
			ThrowBone();
		}

	}
}
