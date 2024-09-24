using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            effector.surfaceArc = 0;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            effector.surfaceArc = 180;
        }
    }


}
