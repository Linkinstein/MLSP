using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassinate : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    PlayerManager pMan;

    void Start()
    {
        pMan = PlayerManager.Instance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!pMan.seen)
            {
                prompt.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && !pMan.attacking && !pMan.isDashing)
                {

                }
            }
            else prompt.SetActive(true);
        }
    }
}
