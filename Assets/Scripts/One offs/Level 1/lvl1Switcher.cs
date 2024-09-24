using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1Switcher : Progressor
{

    [SerializeField] GameObject gate;

    public override void Progress()
    {
        LevelManager lm = LevelManager.instance;
        lm.switches++;
        if (lm.switches >= 4) gate.SetActive(false);
    }
}
