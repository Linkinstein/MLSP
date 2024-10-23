using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private UIManager UIMan;
    Rigidbody2D RB;
    Animator anim;
    [SerializeField] EnemyAI eAI;
    [SerializeField] AudioSource aS;

    void Start()
    {
        UIMan = UIManager.Instance;
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!UIMan.pause && !UIMan.cinematic)
        {
            CheckAnimationState();
        }
    }

    private void CheckAnimationState()
    {
        anim.SetFloat("Vel Y", RB.velocity.y);
        anim.SetBool("Walking", Mathf.Abs(RB.velocity.x) > 0.1f);
        if (Mathf.Abs(RB.velocity.x) > 0.1f)
        {
            if (!aS.isPlaying)
            {
                if (aS.pitch > 0.8f) aS.pitch = 0.8f;
                else aS.pitch = 1f;
                aS.Play();
            }
        }
        anim.SetBool("Running", eAI.checkingSus || eAI.isChasing);

    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }
}
