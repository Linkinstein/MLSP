using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineOrbitalTransposer;
using static UnityEngine.Rendering.DebugUI;

public class DialogueTrigger : MonoBehaviour
{
    private UIManager UIMan;

    [SerializeField] private bool step = false;
    [SerializeField] private DialogueObject dialogueObject;

    private void Start()
    {
        UIMan = UIManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (step)
        {
            if (other.CompareTag("Player"))
            {
                talky();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!step)
        {
            if (collision.tag == "Player")
            {
                if (Input.GetKey(KeyCode.E) && !PlayerInput.instance.attacking && !PlayerInput.instance.IsDashing)
                {
                    talky();
                }
            }
        }
    }

    private void talky()
    {
        UIMan.StartDialogue(dialogueObject);
    }
}
