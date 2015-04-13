using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour {
	public Transform player;
	public int damagePoints = 0;
	
	void Awake ()
	{
		if (gameObject.tag == "fireball")
		{
			transform.LookAt(player);
		}
	}
}
