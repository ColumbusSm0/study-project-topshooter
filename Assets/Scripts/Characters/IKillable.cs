using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable 
{
    void DealDamage(int dano);

    void Heal(int cura);

    void Die();
    
    
}
