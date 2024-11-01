using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAC : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool walking = false;
    [SerializeField] bool running = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartWalking());
    }
    IEnumerator StartWalking()
    {
        float r = Random.Range(0.1f, 5f);
        yield return new WaitForSeconds(r);
        anim.SetBool("Walking", walking);
        anim.SetBool("Running", running);
    }
}
