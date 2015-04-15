using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {
	private Animator animator;

	[HideInInspector]
	public bool collected;

	void Start ()
	{
		animator = gameObject.GetComponent<Animator> ();
		collected = false;
	}

	public void OpenChest ()
	{
		animator.SetBool ("open", true);
		collected = true;
	}
}
