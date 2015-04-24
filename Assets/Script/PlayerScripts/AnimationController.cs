using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationController : MonoBehaviour {
    public RuntimeAnimatorController[] animators;
    public List<GameObject> inventory;

    enum Animation
    {
        IDLE,
        WALK,
        SWING,
        SHOOT
    };
    
    Animation currentAnimation;
    Animator animator;
    PlayerController playerController;
    
	void Start ()
    {
        currentAnimation = Animation.IDLE;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        inventory = playerController.inventory;
	}
	
	void Update ()
    {
        if (Input.GetKey("a") && Input.GetKey("w"))
        {
            UpdateAnimationState(Animation.WALK);
            if (Input.GetButton("Fire1"))
            {
                UpdateAnimationState(Animation.SWING);
            }
        }
        else if (Input.GetKey("a") && Input.GetKey("s"))
        {
            UpdateAnimationState(Animation.WALK);
            if (Input.GetButton("Fire1"))
            {
                UpdateAnimationState(Animation.SWING);
            }
        }
        else if (Input.GetKey("d") && Input.GetKey("w"))
        {
            UpdateAnimationState(Animation.WALK);
            if (Input.GetButton("Fire1"))
                if (Input.GetButton("Fire1"))
                {
                    UpdateAnimationState(Animation.SWING);
                }
        }
        else if (Input.GetKey("d") && Input.GetKey("s"))
        {            
            UpdateAnimationState(Animation.WALK);
            if (Input.GetButton("Fire1"))
            {
                UpdateAnimationState(Animation.SWING);
            }
        }
        else if (Input.GetKey("w"))
        {
            UpdateAnimationState(Animation.WALK);
            if (Input.GetButton("Fire1"))
            {
                UpdateAnimationState(Animation.SWING);
            }
        }
        else if (Input.GetKey("s"))
        {
            UpdateAnimationState(Animation.WALK);
            if (Input.GetButton("Fire1"))
            {
                UpdateAnimationState(Animation.SWING);
            }
        }
        else if (Input.GetKey("a"))
        {
            UpdateAnimationState(Animation.WALK);
            if (Input.GetButton("Fire1"))
            {
                UpdateAnimationState(Animation.SWING);
            }
        }
        else if (Input.GetKey("d"))
        {
            UpdateAnimationState(Animation.WALK);
            if (Input.GetButton("Fire1"))
            {
                UpdateAnimationState(Animation.SWING);
            }
        }
        else
        {
            UpdateAnimationState(Animation.IDLE);
            if (Input.GetButton("Fire1"))
            {
                UpdateAnimationState(Animation.SWING);
            }
        }
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            animator.runtimeAnimatorController = animators[0];
        }
        
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (inventory.Count >= 2)
            {
                animator.runtimeAnimatorController = animators[1];
            }
        }
        
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (inventory.Count >= 3)
            {
                animator.runtimeAnimatorController = animators[2];
            }
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            if (inventory.Count >= 4)
            {
                animator.runtimeAnimatorController = animators[3];
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
            case Animation.SWING:
                animator.SetInteger("animationState", 3);
                break;
        }

        currentAnimation = curAnimState;
    }
}
