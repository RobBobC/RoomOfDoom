using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	protected enum attackType {melee, ranged};
	protected Vector3 direction;
	public float moveSpeed;
	public int health;
}
