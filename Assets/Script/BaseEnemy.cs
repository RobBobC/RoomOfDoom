using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	public float moveSpeed;
	public int health;

	protected enum attackType {
		melee,
		ranged
	};

	protected Vector3 direction;
}
