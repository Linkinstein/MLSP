using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField] private bool hpSmall;
    [SerializeField] private bool hpBig;

    private PlayerManager pMan;

    private void Start()
    {
        pMan = PlayerManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (hpSmall) pMan.hpS++;
            if (hpBig) pMan.hpB++; 

            Destroy(this.gameObject);
        }
    }
}
