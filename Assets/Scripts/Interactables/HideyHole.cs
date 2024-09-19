using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideyHole : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    private bool hiding = false;
    private PlayerAnimator pAnim;
    private PlayerManager pMan;

    private void Start()
    {
        prompt.SetActive(false);
        pAnim = PlayerAnimator.instance;
        pMan = PlayerManager.Instance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prompt.SetActive(true);
            if (Input.GetKey(KeyCode.E) && !PlayerInput.instance.attacking && !PlayerInput.instance.IsDashing)
            {
                pAnim.Hide();
                hiding = true;
            }

            if (!Input.GetKey(KeyCode.E) && hiding)
            {
                pAnim.UnHide();
                hiding = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prompt.SetActive(false);
            pAnim.UnHide();
        }
    }
}
