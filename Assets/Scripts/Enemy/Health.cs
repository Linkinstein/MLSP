using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 6;
    [SerializeField] private EnemyAI eAI;

    private void Update()
    {
        if (health <= 0)
        {
            eAI.GetComponent<Death>().Die();
        }
    }

    public void TakeDamage(int dmg)
    { 
        health -= dmg;
    }
}
