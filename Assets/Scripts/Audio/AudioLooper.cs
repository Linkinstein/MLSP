using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLooper : MonoBehaviour
{
    public AudioSource audioSource; 
    public float minWaitTime = 1f;   
    public float maxWaitTime = 5f;  

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        StartCoroutine(PlayAudioWithRandomDelay());
    }

    private IEnumerator PlayAudioWithRandomDelay()
    {
        while (true)
        {
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
