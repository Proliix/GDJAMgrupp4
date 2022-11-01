using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;

    bool isDead = false;

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Debug.Log("PLAYER IS DEAD IMPLEMENT DEATH");
            isDead = true;
        }
    }



}
