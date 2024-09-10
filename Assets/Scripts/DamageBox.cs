using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public float lifetime;
    public bool fromPlayer = false;
    public int damage;

    void OnEnable()
    {
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    { 
        yield return new WaitForSeconds(lifetime);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fromPlayer && collision.tag == "Enemy")
        { 
            collision.GetComponent<Health>().TakeDamage(damage);
        }


        if (!fromPlayer && collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
