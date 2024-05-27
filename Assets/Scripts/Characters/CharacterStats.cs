using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int InitialLife = 100;
    [HideInInspector]
    public int Life;
    public float Velocity = 7;
    void Awake ()
    {
        Life = InitialLife;
    }

}
