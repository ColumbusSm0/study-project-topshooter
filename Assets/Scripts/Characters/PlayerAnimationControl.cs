using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : CharacterAnimationControl
{
    private void OnEnable()
    {
        WeaponControl.ReloadEvent += ReloadAnim;
        WeaponControl.ShootEvent += ShootAnim;
    }

    private void OnDisable()
    {
        WeaponControl.ReloadEvent -= ReloadAnim;
        WeaponControl.ShootEvent -= ShootAnim;
    }

    private void ShootAnim(bool shoot, float multiplier)
    {
        myAnimator.SetBool("Shoot", shoot);
        myAnimator.SetFloat("RecoilSpeedMultipler", multiplier);
    }

    private void ReloadAnim(bool reload)
    {
        myAnimator.SetBool("Reload", reload);
    }
    private void RecoilFinished()
    {
        myAnimator.SetBool("Shoot", false);
        Debug.Log("Shoot Finished anim control");
    }

    private void ReloadFinished()
    {
        myAnimator.SetBool("Reload", false);
        Debug.Log("Reload Finished");
    }
}
