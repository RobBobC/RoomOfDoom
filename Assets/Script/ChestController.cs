using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {
	private Animator animator;

	public GameObject weaponRewardOne;
	public GameObject weaponRewardTwo;

	[HideInInspector]
	public bool collectable;

	void Start ()
	{
		animator = gameObject.GetComponent<Animator> ();
		collectable = false;
	}

	public void OpenChest ()
	{
		animator.Play ("OpenChest");
		collectable = false;
	}

	public void CloseChest ()
	{
		animator.Play ("IdleChest");
	}
}
