using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : CharacterAnimationControl
{
    private void OnEnable()
    {
        WeaponControl.ReloadEvent += ReloadAnim;
        WeaponControl.ShootAnimEvent += ShootAnim;
    }

    private void OnDisable()
    {
        WeaponControl.ReloadEvent -= ReloadAnim;
        WeaponControl.ShootAnimEvent -= ShootAnim;
    }

    private void ShootAnim(GunScriptableObject GunSO)
    {
        myAnimator.SetBool("Shoot", true);
        myAnimator.SetFloat("RecoilSpeedMultipler", GunSO.SpeedMultiplierRecoilAnimation);
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
