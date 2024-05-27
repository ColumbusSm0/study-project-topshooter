using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour
{

    private Animator myAnimator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void AttackAnim (bool state) 
    {
        myAnimator.SetBool("Atacando", state);
    }

    public void MovementAnim (float movementValue)
    {
        myAnimator.SetFloat("Moving", movementValue);
    }

    public void DeathAnim (bool state)
    {
        int randomInt = Random.Range(0,2);
        myAnimator.SetInteger("MortoIndex", randomInt);
        myAnimator.SetBool("Morto", state);
        
    }
}
