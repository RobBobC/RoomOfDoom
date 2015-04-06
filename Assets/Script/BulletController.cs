using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Obstacle")
			Destroy (gameObject);
	}

}
