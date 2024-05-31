using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : CharacterAnimationControl
{
    private void OnEnable()
    {
        EnemyControl.DamageEvent += ZombieDamageAnim;
    }

    private void OnDisable()
    {
        EnemyControl.DamageEvent -= ZombieDamageAnim;
    }

    public void AttackAnim(bool state)
    {
        myAnimator.SetBool("Atacando", state);
    }

    public void ZombieDamageAnim(Animator myLocalAnimator)
    {
        var randomFloat = Random.Range(.2f, .6f);
        myLocalAnimator.Play("Zombie_Hit", -1, randomFloat);
    }

    public void DeathAnim(bool state)
    {
        EnemyControl.DamageEvent -= ZombieDamageAnim;
        int randomInt = Random.Range(0, 2);
        myAnimator.SetInteger("MortoIndex", randomInt);
        myAnimator.SetBool("Morto", state);
    }

}
