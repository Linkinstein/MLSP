using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    LevelManager lm;
    EnemyAI eAI;
    EnemyShoot eS;
    EnemyAnimator eAnim;
    SuspiciousObject susOb;
    public bool dead = false;
    public bool canSus = false;

    public virtual void Start()
    {
        if (canSus) susOb = GetComponent<SuspiciousObject>();
        lm = LevelManager.instance;
        eAI = GetComponent<EnemyAI>();
        eS = GetComponent<EnemyShoot>();
        eAnim = GetComponent<EnemyAnimator>();
        dead = false;
    }

    public virtual void Die()
    {
        if (canSus) susOb.sussy = true;
        eAnim.Die();
        lm.takedown++;
        eS.enabled = false;
        DisableAllChildren();
        eAI.enabled = false;
        eAnim.enabled = false;
        dead = true;
    }

    public void DisableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
