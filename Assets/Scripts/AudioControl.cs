using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource MusicAudioSource;
    public AudioSource GunAudioSource;
    public static AudioSource instancia;
    public static AudioSource SFXInstancia;

    void Awake()
    {
        // MeuAudioSource = GetComponent<AudioSource>();
        instancia = MusicAudioSource;
        SFXInstancia = GunAudioSource;
    }
}
