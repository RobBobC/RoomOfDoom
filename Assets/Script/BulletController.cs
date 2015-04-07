using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public int damagePoints = 0;

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Obstacle")
			Destroy (gameObject);
	}

	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}
}
