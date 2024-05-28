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

    

    [Header("Ammo and Shooting Stats")]

    public int CurrentMagazineBullets;
    public int CurrentAmmo = 100;

    [Header("Sound Config")]
    public AudioSource WeaponAudioSource;
    [Range(0.1f, 0.5f)]
    public float pitchMultplier;

    public GameObject BulletSpawn1 { get => BulletSpawn; set => BulletSpawn = value; }

    void Start()
    {
        GunInstance = UpdateEquippedWeapon(GunSO);
    }

    void Update()
    {
        if (!GunSO.isAutomatic && Input.GetButtonDown("Fire1") && GunSO.timer > GunSO.delayBetweenShots)
        {
            TryToShoot();
        }

        if (GunSO.isAutomatic && Input.GetButton("Fire1") && GunSO.timer > GunSO.delayBetweenShots)
        {
            TryToShoot();
        }

        if ( Input.GetButtonDown("Reload"))
        {
            Reload();
            Debug.Log("Tried Reload");
        }

        GunSO.timer += Time.deltaTime;

    }

    void TryToShoot()
    {
        if (CurrentMagazineBullets > 0)
        {
            GunSO.timer = 0;
            Instantiate(GunSO.Bullet, BulletSpawn1.transform.position, BulletSpawn1.transform.rotation);
            WeaponAudioSource.pitch = UnityEngine.Random.Range(1 - pitchMultplier, 1 + pitchMultplier);
            WeaponAudioSource.PlayOneShot(GunSO.ShootSound);
            InstanceMuzzleLight.Play();
            InstanceMuzzleFlashEffect.Play();

            CurrentMagazineBullets -= 1;
        } else {

            Reload();
        }
        
    }

    public void Reload()
    {
        if(CurrentMagazineBullets < GunSO.magazineSize)
        {
            int bulletsToReload = GunSO.magazineSize - CurrentMagazineBullets;

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
