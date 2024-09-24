using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioClip BGMAudioStart; 
    public AudioClip BGMAudioLoop; 

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = BGMAudioStart;
        audioSource.Play();

        Invoke("StartLoop", BGMAudioStart.length);
    }

    void StartLoop()
    {
        audioSource.clip = BGMAudioLoop;
        audioSource.loop = true; 
        audioSource.Play();
    }
}
