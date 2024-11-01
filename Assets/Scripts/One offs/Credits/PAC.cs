using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject t;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartWalking());
    }

    IEnumerator StartWalking()
    {
        yield return new WaitForSeconds(6f);
        t.SetActive(false);
        anim.SetBool("Walking", true);
    }
}
