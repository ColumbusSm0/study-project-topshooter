using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour
{

    private Animator myAnimator;

    private void OnEnable()
    {
        if (GetComponent<WeaponControl>() != null)
        {
            WeaponControl.ReloadEvent += ReloadAnim;
            WeaponControl.ShootEvent += ShootAnim;
        }

        if (GetComponent<EnemyControl>() != null)
        {
            EnemyControl.DamageEvent += ZombieDamageAnim;
        }

    }

    private void OnDisable()
    {
        if (GetComponent<WeaponControl>() != null)
        {
            WeaponControl.ReloadEvent -= ReloadAnim;
            WeaponControl.ShootEvent -= ShootAnim;
        }

        if (GetComponent<EnemyControl>() != null)
        {
            EnemyControl.DamageEvent -= ZombieDamageAnim;
        }
    }

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void AttackAnim(bool state)
    {
        myAnimator.SetBool("Atacando", state);
    }

    public void MovementAnim(float movementValue)
    {
        myAnimator.SetFloat("Moving", movementValue);
    }

    public void ShootAnim(bool shoot, float multiplier)
    {
        myAnimator.SetBool("Shoot", shoot);
        myAnimator.SetFloat("RecoilSpeedMultipler", multiplier);
    }

    public void ReloadAnim(bool reload)
    {
        myAnimator.SetBool("Reload", reload);
    }

    public void ZombieDamageAnim()
    {
        myAnimator.Play("Zombie_Hit", -1, 0f);
    }

    public void DeathAnim(bool state)
    {
        EnemyControl.DamageEvent -= ZombieDamageAnim;
        int randomInt = Random.Range(0, 2);
        myAnimator.SetInteger("MortoIndex", randomInt);
        myAnimator.SetBool("Morto", state);
    }

    void RecoilFinished()
    {
        myAnimator.SetBool("Shoot", false);
        Debug.Log("Shoot Finished anim control");
    }

    void ReloadFinished()
    {
        myAnimator.SetBool("Reload", false);
        Debug.Log("Reload Finished");
    }
}
