using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 6;
    [SerializeField] private EnemyAI eAI;

    private void Update()
    {
        if (health <= 0 && Time.timeScale != 0)
        {
            eAI.GetComponent<Death>().Die();
        }
    }

    public void TakeDamage(int dmg)
    { 
        health -= dmg;
        Time.timeScale = 0f;
        StartCoroutine(HitStop());
    }

    IEnumerator HitStop()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1f;
    }
}
