using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	private enum attackType {melee, ranged};
	private Vector2 direction;
	public float moveSpeed;
	public int health;
}
