using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_OpenTrain : MonoBehaviour
{
    public GameObject door;

    void OnDestroy()
    {
        door.SetActive(false);
    }
}
