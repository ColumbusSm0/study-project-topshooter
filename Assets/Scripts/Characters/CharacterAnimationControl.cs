using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour
{

    protected Animator myAnimator;


    public virtual void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void MovementAnim(float movementValue)
    {
        myAnimator.SetFloat("Moving", movementValue);
    }
}
