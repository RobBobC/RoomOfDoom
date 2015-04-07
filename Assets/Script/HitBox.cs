using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour {

	public int damagePoints;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Rat")
		{
			other.GetComponent<RatController>().health -= damagePoints;
		}
	}
}
