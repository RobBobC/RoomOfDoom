using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	public enum attackType {melee, ranged};
	protected Vector3 direction;
	public float moveSpeed;
	public int health;
}
