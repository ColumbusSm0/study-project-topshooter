using System;
using System.Threading;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "GunScriptableObject", menuName = "NewGun", order = 0)]
public class GunScriptableObject : ScriptableObject
{

    public String GunName;
    public GameObject GunPrefab;
    [SerializeField] public GameObject Bullet;
    [SerializeField] public GameObject MuzzleFlashEffect;
    [SerializeField] public Animation MuzzleLight;

    public bool isAutomatic;

    public float timer;
    [Range(0.1f, 1.5f)] public float DelayBetweenShots;
    public float SpeedMultiplierRecoilAnimation;

    public int magazineSize;
    [Range(0.5f, 2f)] public float ReloadTime;

    public int maxAmmo;

    // [Range(0.1f,1.5f)]public float fireRate;

    public AudioClip ShootSound;

    private void OnEnable()
    {
        SpeedMultiplierRecoilAnimation = 0.5f / DelayBetweenShots;
        timer = 0;
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    private void ModeChanged(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.EnteredEditMode)
        {
            timer = 0;
        }
    }


}




