using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private DialogueManager dMan;
    private EndScreenManager esMan;
    private PASystem PASys;

    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject TabUI;
    [SerializeField] private GameObject RetryUI;

    private bool _pause = false;
    public bool cinematic = false;
    private bool pauseOn = false;
    public bool tabOn = false;

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

    private void Start()
    {
        dMan = DialogueManager.Instance;
        esMan = EndScreenManager.Instance;
        PASys = PASystem.Instance;
    }

    void Update()
    {
        if (!cinematic)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (tabOn) UnTab();
                else if (!pauseOn) Pause();
                else Unpause();
            }

            if (Input.GetKeyDown(KeyCode.Tab) && !pauseOn)
            {
                if (!tabOn) Tab();
                else UnTab();
            }
        }
    }

    public void Pause()
    {
        pause = true;
        PauseUI.SetActive(true);
        pauseOn = true;
    }

    public void Unpause()
    {
        pause = false;
        PauseUI.SetActive(false);
        pauseOn = false;
    }

    public void Tab()
    {
        pause = true;
        TabUI.SetActive(true);
        tabOn = true;
    }

    public void UnTab()
    {
        pause = false;
        TabUI.SetActive(false);
        tabOn = false;
    }
    public void End(string time, int spd, int alertos, int hiTakis, int takis, bool nills, bool nalerts, bool perfsta, int totesco)
    {
        cinematic = true;
        esMan.End(time, spd, alertos, hiTakis, takis, nills, nalerts, perfsta, totesco);
    }

    public void StartDialogue(DialogueObject diOb)
    {
        dMan.SetDiOb(diOb);
        dMan.On();
        cinematic = true;
    }

    public void Back2Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PABark(string barkType)
    {
        switch (barkType)
        {
            case "Alert":
                PASys.Alert();
                break;
            case "Sussy":
                PASys.Sustivity();
                break;
            case "Body":
                PASys.BodyFound();
                break;
        }
    }

    public void DharmanEnding()
    { 
        dMan.ending = true;
    }

    public void Dead()
    {
        cinematic = true;
        RetryUI.SetActive(true);
    }
}
