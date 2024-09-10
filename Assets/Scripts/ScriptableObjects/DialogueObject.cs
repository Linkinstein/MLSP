using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueObject", menuName = "DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] public string[] dialogues;
    [SerializeField] public string[] rightName;
    [SerializeField] public string[] leftName;
    [SerializeField] public Sprite[] rightPortrait;
    [SerializeField] public Sprite[] leftPortrait;
    [SerializeField] public bool[] rightTalking;
}
