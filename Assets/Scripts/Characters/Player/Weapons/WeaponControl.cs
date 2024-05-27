using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.VFX;

public class WeaponControl : MonoBehaviour
{
    [Header("Game Objects and dependecies")]
    // [SerializeField]private GameObject Bullet;
    [SerializeField] private GameObject BulletSpawn;

    public GameObject GunHolder;
    public GameObject GunInstance;
    [SerializeField]private VisualEffect InstanceMuzzleFlashEffect;
    [SerializeField]private Animation InstanceMuzzleLight;
    public GunScriptableObject GunSO;

    // [Header("Ammo and Shooting Stats")]
    // [SerializeField]private bool isAutomatic;

    // [SerializeField]private int magazineSize;

    // [SerializeField]private int maxAmmo;

    // private float timer;

    // [Range(0.1f,1.5f)][SerializeField]private float delayBetweenShots;

    [Header("Sound Config")]
    public AudioSource WeaponAudioSource;
    [Range(0.1f, 0.5f)]
    public float pitchMultplier;


    // Start is called before the first frame update
    void Start()
    {
        GunInstance = UpdateEquippedWeapon(GunSO);
        // SetGunEffects(GunInstance);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GunSO.isAutomatic && Input.GetButtonDown("Fire1") && GunSO.timer > GunSO.delayBetweenShots)
        {
            Shoot();
        }

        if (GunSO.isAutomatic && Input.GetButton("Fire1") && GunSO.timer > GunSO.delayBetweenShots)
        {
            Shoot();
        }

        GunSO.timer += Time.deltaTime;

    }

    void Shoot()
    {
        GunSO.timer = 0;
        Instantiate(GunSO.Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        WeaponAudioSource.pitch = Random.Range(1 - pitchMultplier, 1 + pitchMultplier);
        WeaponAudioSource.PlayOneShot(GunSO.ShootSound);
        InstanceMuzzleLight.Play();
        InstanceMuzzleFlashEffect.Play();
        
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
        SetGunEffects(GunInstance);
        return GunInstance;

    }

    public void SetGunEffects(GameObject GunInstance)
    {
        InstanceMuzzleFlashEffect = GunInstance.GetComponentInChildren<VisualEffect>();
        InstanceMuzzleLight = GunInstance.GetComponentInChildren<Animation>();
    }
}
