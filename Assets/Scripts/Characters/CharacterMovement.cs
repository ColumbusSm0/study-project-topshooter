using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
   private Rigidbody myRigidbody;
   
   void Awake ()
   {
       myRigidbody = GetComponent<Rigidbody>();
   }
   
   public void Movement(Vector3 direction, float velocity) 
   {
       myRigidbody.MovePosition(myRigidbody.position + direction.normalized * velocity * Time.deltaTime);
   }

   public void Rotation (Vector3 direction)
   {
        Quaternion novaRotacao = Quaternion.LookRotation(direction);
        myRigidbody.MoveRotation(novaRotacao);
   }

   public void DisableRB()
   {
    Destroy(myRigidbody);
    Destroy(this);
   }
}
