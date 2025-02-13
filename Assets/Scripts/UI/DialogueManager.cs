using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    private UIManager UIMan;

    public bool ending = false;

    public DialogueObject diOb;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private Image rightPortrait;
    [SerializeField] private Image leftPortrait;
    [SerializeField] private TextMeshProUGUI rightName;
    [SerializeField] private TextMeshProUGUI leftName;

    public static DialogueManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIMan = UIManager.Instance;
    }


    private bool active = false;
    private int index = 0;

    private void Update()
    {
        if (active) UIMan.pause = true;

        if (active && Input.GetKeyDown(KeyCode.Space) || active && Input.GetMouseButtonDown(0)) NextClicked();

        if (active && Input.GetKey(KeyCode.LeftShift) || active && Input.GetKey(KeyCode.RightShift)) NextClicked();
    }

    private void NextClicked()
    {
        index++;
        if (index >= diOb.dialogues.Length) Off();
        else Next();
    }

    public void On()
    {
        UIMan.pause = true;
        active = true;
        dialogueUI.SetActive(true);
        index = 0;
        Next();
    }

    private void Next()
    {
        dialogue.SetText(diOb.dialogues[index]);
        rightName.SetText(diOb.rightName[index]);
        leftName.SetText(diOb.leftName[index]);
        rightPortrait.sprite = diOb.rightPortrait[index];
        leftPortrait.sprite = diOb.leftPortrait[index];

        if (diOb.rightTalking[index])
        {
            rightPortrait.color = Color.white;
            leftPortrait.color = Color.grey;
        }
        else
        {
            rightPortrait.color = Color.grey;
            leftPortrait.color = Color.white;
        }
    }

    private void Off()
    {
        UIMan.cinematic = false;
        UIMan.pause = false;
        active = false;
        dialogueUI.SetActive(false);
        if(ending) LevelManager.instance.End();
    }

    public void SetDiOb(DialogueObject dobby)
    {
        active = false;
        diOb = dobby;
    }
}
