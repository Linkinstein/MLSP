using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    LevelManager lm;

    public virtual void Start()
    {
        lm = LevelManager.instance;
    }

    public virtual void Die()
    {
        lm.takedown++;
        Destroy(gameObject);
    }
}
