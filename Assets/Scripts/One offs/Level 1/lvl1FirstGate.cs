using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1FirstGate : Progressor
{
    [SerializeField] GameObject gate;


    public override void Progress()
    {
        gate.SetActive(false);
    }
}
