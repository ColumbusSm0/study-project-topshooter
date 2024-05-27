using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponControl : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject BulletSpawn;

    public GameObject MuzzleFlashEffect;
    public Animation MuzzleLight;

    public AudioClip ShootSound;
    [Range(0.1f,0.5f)]
    public float pitchMultplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bullet,BulletSpawn.transform.position, BulletSpawn.transform.rotation);
            AudioControl.SFXInstancia.pitch = Random.Range(1-pitchMultplier,1+pitchMultplier);
            AudioControl.SFXInstancia.PlayOneShot(ShootSound);
            MuzzleLight.Play();
            MuzzleFlashEffect.GetComponent<VisualEffect>().Play();
        }
    }
}
