using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    private void OnDestroy()
    {
        DialogueManager.Instance.ending = true;
    }
}
