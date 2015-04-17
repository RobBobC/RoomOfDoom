using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	public enum attackType {
		melee,
		ranged
	};

	public attackType type;
	public GameObject weapon;
}
