using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject PlayerArea;
    public bool highAlert = false;

    public int reinforcementNo;

    void Start()
    {
        instance = this;    
    }

    void Update()
    {
        
    }
}
