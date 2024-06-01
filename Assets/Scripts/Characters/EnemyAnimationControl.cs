using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAnimationControl : CharacterAnimationControl
{
    private EnemyControl enemyControl;
    public override void Awake()
    {
        base.Awake();
        enemyControl = GetComponent<EnemyControl>();
    }
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

    public void ZombieDamageAnim(Animator myLocalAnimator, Vector2 DamageDirection)
    {
        AnimatorStateInfo stateInfo = myLocalAnimator.GetCurrentAnimatorStateInfo(0);
        SetAnimDirection(myLocalAnimator, DamageDirection);
        PlayAnimDirect(myLocalAnimator, stateInfo);
        // SetHitTrigger();

    }

    public void SetAnimDirection(Animator animator, Vector2 direction)
    {
        animator.SetFloat("HitDirectionX", direction.x);
        animator.SetFloat("HitDirectionY", direction.y);
    }

    public void SetHitTrigger(Animator animator)
    {
        animator.SetTrigger("Hit");
    }

    public void PlayAnimDirect(Animator animator, AnimatorStateInfo stateInfo)
    {
        var randomFloat = Random.Range(.3f, .5f);
        var rewindValue = 0f;

        if (stateInfo.IsName("Zombie_Hit"))
        {
            if (stateInfo.normalizedTime > 0.7)
            {
                rewindValue = randomFloat;
            }
            else
            {
                rewindValue = stateInfo.normalizedTime / 7;
                var stateLenght = stateInfo.length;
                rewindValue = ((stateInfo.normalizedTime / 7) * stateLenght);
            }

            animator.Play("Zombie_Hit", 0, stateInfo.normalizedTime - rewindValue);
        }
        else
        {
            animator.Play("Zombie_Hit", 0, 0f);
        }
    }

    public void DepecratedAnim(Animator myLocalAnimator, Vector2 DamageDirection)
    {
        AnimatorStateInfo stateInfo = myLocalAnimator.GetCurrentAnimatorStateInfo(0);

        var randomFloat = Random.Range(.1f, .2f);
        myLocalAnimator.SetFloat("HitDirectionX", DamageDirection.x);
        myLocalAnimator.SetFloat("HitDirectionY", DamageDirection.y);

        if (stateInfo.IsName("Zombie_Hit"))
        {
            var rewindValue = stateInfo.normalizedTime / 7;
            var stateLenght = stateInfo.length;
            rewindValue = ((stateInfo.normalizedTime / 7) * stateLenght) + Random.Range(-0.05f, 0.05f);
            myLocalAnimator.Play("Zombie_Hit", 0, stateInfo.normalizedTime - rewindValue);
        }
        else
        {
            myLocalAnimator.Play("Zombie_Hit", 0, randomFloat);
        }
    }


    void OnAnimationEnd()
    {
        enemyControl.HitFinished();
        Debug.Log("hit finished");
    }

    public void DeathAnim(bool state)
    {
        EnemyControl.DamageEvent -= ZombieDamageAnim;
        int randomInt = Random.Range(0, 2);
        myAnimator.SetInteger("MortoIndex", randomInt);
        myAnimator.SetBool("Morto", state);
    }

}
