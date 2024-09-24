using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassinate : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    [SerializeField] private Death d;
    [SerializeField] private AudioSource aS;
    PlayerManager pMan;
    bool killable = false;

    void Start()
    {
        pMan = PlayerManager.Instance;
    }

    private void Update()
    {
        if (killable && Input.GetKeyDown(KeyCode.E) && !pMan.attacking && !pMan.isDashing)
        {
            aS.Play();
            d.Die();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!pMan.seen)
            {
                prompt.SetActive(true);
                killable = true;
            }
            else
            { 
                prompt.SetActive(false);
                killable = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            prompt.SetActive(false);
            killable = false;
        }
    }
}
