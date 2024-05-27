using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : CharacterMovement
{
    public void PlayerRotation (LayerMask FloorMask) 
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin,raio.direction * 100, Color.red);

        RaycastHit impact;

        if(Physics.Raycast(raio, out impact, 100, FloorMask))
        {
            Vector3 posicaoMiraPlayer = impact.point - transform.position;

            posicaoMiraPlayer.y = transform.position.y;

            Rotation(posicaoMiraPlayer);

        }
    }
}
