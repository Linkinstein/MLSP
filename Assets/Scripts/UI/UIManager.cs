using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private DialogueManager dMan;
    private EndScreenManager esMan;
    private PASystem PASys;

    [SerializeField] private GameObject PauseUI;

    private bool _pause = false;
    public bool cinematic = false;
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

    private void Start()
    {
        dMan = DialogueManager.Instance;
        esMan = EndScreenManager.Instance;
        PASys = PASystem.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !cinematic)
        {
            if (!pauseOn) Pause();
            else Unpause();
            
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

    public void End(string time, int spd, int alertos, int hiTakis, int takis, bool nills, bool nalerts, bool perfsta, int totesco)
    {
        esMan.End(time, spd, alertos, hiTakis, takis, nills, nalerts, perfsta, totesco);
    }

    public void StartDialogue(DialogueObject diOb)
    {
        dMan.SetDiOb(diOb);
        dMan.On();
    }

    public void Exit()
    {
        Application.Quit();
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
}
