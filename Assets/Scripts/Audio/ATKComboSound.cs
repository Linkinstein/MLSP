using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKComboSound : MonoBehaviour
{
    public static ATKComboSound Instance;
    [SerializeField] AudioSource as1;
    [SerializeField] AudioSource as2;
    [SerializeField] AudioSource as3;

    private void Awake()
    {
        Instance = this;
    }

    public void hit1()
    {
        as1.Play();
    }

    public void hit2()
    {
        as2.Play();
    }

    public void hit3()
    {
        as3.Play();
    }
}
