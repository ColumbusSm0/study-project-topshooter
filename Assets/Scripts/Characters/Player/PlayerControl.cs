using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour, IKillable
{
    private Vector3 direction;
    public LayerMask FloorMask;
    public GameObject TextGameOver;
    public bool isAlive = true;
    public HUDControl ScriptHudControl;
    public AudioClip DamageSound;

    private PlayerMovementControl myPlayerMovement;

    private CharacterAnimationControl myAnimationControl;
    
    public CharacterStats myPlayerStats;
    void Start()
    {
        myPlayerMovement = GetComponent<PlayerMovementControl>();

        myAnimationControl = GetComponent<CharacterAnimationControl>();

        myPlayerStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        

        direction = new Vector3(eixoX,0,eixoZ);
        // Debug.Log("Direction Vector: " + direction);

        myAnimationControl.MovementAnim(direction.magnitude);

    }
    void FixedUpdate() 
    {
        myPlayerMovement.Movement(direction, myPlayerStats.Velocity);

        myPlayerMovement.PlayerRotation(FloorMask);
    }


    public void Heal (int cura)
    {
        myPlayerStats.Life += cura;
        ScriptHudControl.UpdateSliderPlayerLife();
        // AudioControl.instancia.PlayOneShot(HealSound);

        if(myPlayerStats.Life >= myPlayerStats.InitialLife)
        {
            myPlayerStats.Life = myPlayerStats.InitialLife;
        }
    }

    public void DealDamage(int dano)
    {
        myPlayerStats.Life -= dano;
        ScriptHudControl.UpdateSliderPlayerLife();
        AudioControl.instancia.PlayOneShot(DamageSound);

        if(myPlayerStats.Life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ScriptHudControl.GameOver();
        isAlive = false;
    }
}
