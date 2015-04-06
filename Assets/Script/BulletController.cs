using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public int damagePoints = 1;

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Obstacle")
			Destroy (gameObject);
	}
}
