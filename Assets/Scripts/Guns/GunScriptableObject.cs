using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "GunScriptableObject", menuName = "NewGun", order = 0)]
public class GunScriptableObject : ScriptableObject {
    
    public String GunName;
    public GameObject GunPrefab;
    [SerializeField]public GameObject Bullet;
    [SerializeField]public GameObject MuzzleFlashEffect;
    [SerializeField]public Animation MuzzleLight;

    public bool isAutomatic;

    public float timer;
    [Range(0.1f,1.5f)]public float delayBetweenShots;

    public int magazineSize;

    public int maxAmmo;

    // [Range(0.1f,1.5f)]public float fireRate;

    public AudioClip ShootSound;    

    private void OnEnable() {
        timer = 0;
    }
   
}




