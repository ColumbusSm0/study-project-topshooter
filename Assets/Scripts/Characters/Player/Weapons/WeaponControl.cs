using System;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] private GameObject BulletSpawn;
    public GameObject GunHolder;
    public GameObject GunInstance;
    [SerializeField]private VisualEffect InstanceMuzzleFlashEffect;
    [SerializeField]private Animation InstanceMuzzleLight;
    public GunScriptableObject GunSO;

    [SerializeField] private CharacterAnimationControl AnimControl;
   

    [Header("Ammo and Shooting Stats")]
    public int CurrentMagazineBullets;
    public int CurrentAmmo = 100;

    private int bulletsToReload;

    [Header("Sound Config")]
    public AudioSource WeaponAudioSource;
    [Range(0.1f, 0.5f)]
    public float pitchMultplier;

    public GameObject BulletSpawn1 { get => BulletSpawn; set => BulletSpawn = value; }

    // [Header("Actions and Events")]
    #region Actions and Events
    public static event Action<bool> ReloadEvent;
    public static event Action<bool, float> ShootEvent;

    #endregion

    void Start()
    {
        GunInstance = UpdateEquippedWeapon(GunSO); 
    }

    void Update()
    {
        if (!GunSO.isAutomatic && Input.GetButtonDown("Fire1") && GunSO.timer > GunSO.DelayBetweenShots)
        {
            TryToShoot();
        }

        if (GunSO.isAutomatic && Input.GetButton("Fire1") && GunSO.timer > GunSO.DelayBetweenShots)
        {
            TryToShoot();
        }

        if ( Input.GetButtonDown("Reload"))
        {
            OnReload();
            Debug.Log("Tried Reload");
        }

        GunSO.timer += Time.deltaTime;

    }

    void TryToShoot()
    {
        if (CurrentMagazineBullets > 0)
        {
            GunSO.timer = 0;

            ShootEvent?.Invoke(true,GunSO.SpeedMultiplierRecoilAnimation);

            Instantiate(GunSO.Bullet, BulletSpawn1.transform.position, BulletSpawn1.transform.rotation);

            WeaponAudioSource.pitch = UnityEngine.Random.Range(1 - pitchMultplier, 1 + pitchMultplier);
            WeaponAudioSource.PlayOneShot(GunSO.ShootSound);

            InstanceMuzzleLight.Play();
            InstanceMuzzleFlashEffect.Play();

            CurrentMagazineBullets -= 1;
        } else {

            OnReload();
        }
        
    }

    public void OnReload()
    {
        if(CurrentMagazineBullets < GunSO.magazineSize)
        {
            bulletsToReload = GunSO.magazineSize - CurrentMagazineBullets;

            if(CurrentAmmo >= bulletsToReload)
            {
                ReloadEvent?.Invoke(true);
                Debug.Log("Animation Started : Reloaded");
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

    void CompleteReload ()
    {
        if(CurrentAmmo > bulletsToReload)
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
    }

    //Called by Animavion Event inside Reload Animation
    void ReloadFinished()
    {
        CompleteReload();
    }
    //Called by Animavion Event inside Recoil Animation
    void RecoilFinished()
    {
        ShootEvent?.Invoke(false,GunSO.SpeedMultiplierRecoilAnimation);
    }

    public GameObject UpdateEquippedWeapon(GunScriptableObject newGun)
    {
        GunSO = newGun;
        GameObject g = GunHolder;
        for (var i = g.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(g.transform.GetChild(i).gameObject);
        }
        GameObject GunInstance = Instantiate(newGun.GunPrefab,GunHolder.transform.position,GunHolder.transform.rotation,GunHolder.transform);
        SetGunStats();
        SetGunEffects(GunInstance);
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
