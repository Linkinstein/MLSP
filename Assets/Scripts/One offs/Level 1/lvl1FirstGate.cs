using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1FirstGate : Progressor
{
    [SerializeField] GameObject[] nextObj;
    [SerializeField] GameObject gate;


    public override void Progress()
    {
        foreach (GameObject obj in nextObj)
        {
            obj.SetActive(true);
        }
        gate.SetActive(false);
    }
}
