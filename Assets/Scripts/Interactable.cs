using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public GameObject prompt;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        prompt.SetActive(true);
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        prompt.SetActive(false);
    }
}
