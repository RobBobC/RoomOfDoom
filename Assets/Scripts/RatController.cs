using UnityEngine;
using System.Collections;

public class RatController : BaseEnemy {
	
	attackType attack;
	GameObject player;
	float lookAtAngle;

	void Start ()
	{
		attack = attackType.melee;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update ()
	{
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		direction = player.transform.position - transform.position;
		lookAtAngle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (lookAtAngle - 90, Vector3.forward);
	}
}
