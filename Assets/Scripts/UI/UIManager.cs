using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private bool _pause = false;
    [SerializeField] private GameObject PauseUI;
    private bool pauseOn = false;
    public bool pause
    {
        get { return _pause; }
        set 
        { 
            if (value) Time.timeScale = 0;
            else Time.timeScale = 1;
            _pause = value; 
        }
    }

    private void Awake()
    {
        Instance = this;
        pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseOn)
            {
                pause = true;
                PauseUI.SetActive(true);
                pauseOn  = true;
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Unpause()
    {
        pause = false;
        PauseUI.SetActive(false);
        pauseOn = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
