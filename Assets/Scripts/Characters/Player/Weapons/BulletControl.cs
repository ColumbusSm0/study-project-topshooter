using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private Rigidbody bulletRigid;
    public float Velocidade = 50;

    public int myDamage = 1;

    public AudioClip ZombieKill;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletRigid.MovePosition(bulletRigid.position + transform.forward * Velocidade * Time.deltaTime);

    }

    void OnTriggerEnter(Collider collisionObject)
    {
        if(collisionObject.tag == "Inimigo")
        {
           collisionObject.GetComponent<EnemyControl>().DealDamage(myDamage);
        }

        Destroy(gameObject);
    }
}
