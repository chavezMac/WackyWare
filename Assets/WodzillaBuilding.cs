using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WodzillaBuilding : MonoBehaviour
{
    public int hp = 100;

    // Update is called once per frame
    void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Crumble();
        }
        else
        {
            //play wobble animation
        }
    }

    void Crumble()
    {
        
    }
}
