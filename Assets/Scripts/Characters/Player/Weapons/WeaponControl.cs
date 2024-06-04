using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class WeaponControl : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject BulletSpawn;

    public GameObject MuzzleFlashEffect;
    public Animation MuzzleLight;

    public AudioClip ShootSound;
    [Range(0.1f, 0.5f)]
    public float pitchMultplier;
    public int CurrentMagazineBullets;
    public int CurrentAmmo;
    private bool isReloading;
    private int bulletsToReload;
    public AudioSource WeaponAudioSource;

    public GunScriptableObject GunSO;
    private VisualEffect InstanceMuzzleFlashEffect;
    private Animation InstanceMuzzleLight;
    [SerializeField] private GameObject GunHolder;

    // [Header("Actions and Events")]
    #region Actions and Events
    public static event Action<bool> ReloadEvent;
    public static event Action<GunScriptableObject, int, int> ReloadFinishedEvent;
    public static event Action<GunScriptableObject, int, int> ShootHUDEvent;
    public static event Action<GunScriptableObject> ShootAnimEvent;

    #endregion

    void Start()
    {
        UpdateEquippedWeapon(GunSO);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && GunSO.timer > GunSO.DelayBetweenShots)
        {
            TryToShoot();
        }

        if (GunSO.isAutomatic && Input.GetButton("Fire1") && GunSO.timer > GunSO.DelayBetweenShots)
        {
            TryToShoot();
        }

        if (Input.GetButtonDown("Reload") && !isReloading)
        {
            Debug.Log("Tried Reload");
            OnReload();
        }

        GunSO.timer += Time.deltaTime;

    }

    void TryToShoot()
    {
        if (BulletSpawn == null)
        {
            BulletSpawn = GameObject.FindGameObjectWithTag(Tags.BulletSpawn);
        }

        if (!isReloading)
        {
            if (CurrentMagazineBullets > 0)
            {
                GunSO.timer = 0;

                ShootAnimEvent?.Invoke(GunSO);

                Instantiate(GunSO.Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);

                WeaponAudioSource.pitch = Random.Range(1 - pitchMultplier, 1 + pitchMultplier);
                WeaponAudioSource.PlayOneShot(GunSO.ShootSound);

                InstanceMuzzleLight.Play();
                InstanceMuzzleFlashEffect.Play();

                CurrentMagazineBullets -= 1;

                ShootHUDEvent?.Invoke(GunSO, CurrentMagazineBullets, CurrentAmmo);

            }
            else
            {

                WeaponAudioSource.PlayOneShot(GunSO.ClickSound);
                GunSO.timer = 0;
            }
        }

    }

    public void OnReload()
    {
        if (CurrentMagazineBullets < GunSO.magazineSize)
        {
            isReloading = true;
            if (CurrentAmmo > 0)
            {
                ReloadEvent?.Invoke(true);
                Debug.Log("Animation Started : Reloaded");
                WeaponAudioSource.PlayOneShot(GunSO.ReloadSound);
                
            }
            else
            {
                Debug.Log("No Ammo!");
            }
        }
        else
        {
            Debug.Log("Magazine Full");
            return;
        }

    }

    void CompleteReload()
    {
        bulletsToReload = GunSO.magazineSize - CurrentMagazineBullets;

        if (CurrentAmmo >= bulletsToReload)
        {
            CurrentMagazineBullets += bulletsToReload;
            CurrentAmmo -= bulletsToReload;
            Debug.Log("Reloaded");

        }
        else if (CurrentAmmo > 0)
        {
            CurrentMagazineBullets += CurrentAmmo;
            CurrentAmmo = 0;
            Debug.Log("Reloaded and out of ammo");
        }
        isReloading = false;
        ReloadFinishedEvent?.Invoke(GunSO, CurrentMagazineBullets, CurrentAmmo);
    }

    //Called by Animavion Event inside Reload Animation
    void ReloadFinished()
    {
        CompleteReload();
    }
    //Called by Animavion Event inside Recoil Animation
    // void RecoilFinished()
    // {
    //     ShootAnimEvent?.Invoke(GunSO);
    // }

    public GameObject UpdateEquippedWeapon(GunScriptableObject newGun)
    {
        GunSO = newGun;
        GameObject g = GunHolder;
        for (var i = g.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(g.transform.GetChild(i).gameObject);
        }
        GameObject GunInstance = Instantiate(newGun.GunPrefab, GunHolder.transform.position, GunHolder.transform.rotation, GunHolder.transform);
        SetGunStats();
        SetGunEffects(GunInstance);
        ReloadFinishedEvent?.Invoke(GunSO, CurrentMagazineBullets, CurrentAmmo);
        return GunInstance;


    }

    private void SetGunStats()
    {
        CurrentMagazineBullets = GunSO.magazineSize;
    }

    public void SetGunEffects(GameObject GunInstance)
    {
        InstanceMuzzleFlashEffect = GunInstance.GetComponentInChildren<VisualEffect>();
        InstanceMuzzleLight = GunInstance.GetComponentInChildren<Animation>();
    }
}
