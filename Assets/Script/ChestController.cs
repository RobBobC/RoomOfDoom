using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {
	public GameObject weaponRewardOne;
	public GameObject weaponRewardTwo;

	[HideInInspector]
	public bool collectable;

    Animator animator;
    
	void Start ()
	{
		animator = gameObject.GetComponent<Animator> ();
		collectable = false;
	}

    public void OpenChest()
    {
        animator.Play("OpenChest");
        collectable = false;
    }

    public void CloseChest()
    {
        animator.Play("IdleChest");
    }
}
