using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationController : MonoBehaviour {
    public RuntimeAnimatorController[] animators;
    public List<GameObject> inventory;

    enum Animation
    {
        IDLE,
        WALK
    };

    Animation currentAnimation;
    Animator animator;
    
	void Start ()
    {
        currentAnimation = Animation.IDLE;
        animator = GetComponent<Animator>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().inventory;
	}
	
	void Update ()
    {
        if (Input.GetKey("a") && Input.GetKey("w"))
        {
            UpdateAnimationState(Animation.WALK);            
        }
        else if (Input.GetKey("a") && Input.GetKey("s"))
        {
            UpdateAnimationState(Animation.WALK);           
        }
        else if (Input.GetKey("d") && Input.GetKey("w"))
        {
            UpdateAnimationState(Animation.WALK);           
        }
        else if (Input.GetKey("d") && Input.GetKey("s"))
        {            
            UpdateAnimationState(Animation.WALK);
        }
        else if (Input.GetKey("w"))
        {
            UpdateAnimationState(Animation.WALK);
        }
        else if (Input.GetKey("s"))
        {
            UpdateAnimationState(Animation.WALK);
        }
        else if (Input.GetKey("a"))
        {
            UpdateAnimationState(Animation.WALK);
        }
        else if (Input.GetKey("d"))
        {
            UpdateAnimationState(Animation.WALK);
        }
        else
        {
            UpdateAnimationState(Animation.IDLE);
        }
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            animator.runtimeAnimatorController = animators[0];
        }
        
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (inventory.Count > 1)
            {
                animator.runtimeAnimatorController = animators[1];
            }
        }
        
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (inventory.Count > 1)
            {
                animator.runtimeAnimatorController = animators[1];
            }
        }
	}

    void UpdateAnimationState(Animation curAnimState)
    {
        if (currentAnimation == curAnimState)
            return;

        switch (curAnimState)
        {
            case Animation.IDLE:
                animator.SetInteger("animationState", 0);
                break;
            case Animation.WALK:
                animator.SetInteger("animationState", 1);
                break;
        }

        currentAnimation = curAnimState;
    }
}
