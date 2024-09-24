using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator instance;

    private UIManager UIMan;
    private PlayerManager pMan;

    public Animator anim;
    private SpriteRenderer sr;
    public GameObject[] CQCBox;
    public BoxCollider2D VisBox;
    public BoxCollider2D DashBox;

    public bool trash
    {
        get { return pMan.trash; }
        set { pMan.trash = value; }
    }

    public bool canMove
    {
        get {return pMan.canMove;}
        set { pMan.canMove = value; }
    }

    public bool attacking
    {
        get { return pMan.attacking; }
        set { pMan.attacking = value; }
    }

    [Header("Particle FX")]
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject landFX;

    public bool startedJumping {  private get; set; }
    public bool justLanded { private get; set; }
    public bool dash { private get; set; }

    public float currentVelY;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIMan = UIManager.Instance;
        pMan = PlayerManager.Instance;
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = sr.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!UIMan.pause && !UIMan.cinematic)
        {
            CheckAnimationState();
        }
    }

    private void CheckAnimationState()
    {
        anim.SetFloat("Vel Y", pMan.RB.velocity.y);
        anim.SetBool("Trash", trash);
        anim.SetBool("Walking", Mathf.Abs(pMan.RB.velocity.x) > 0.1f);
        if (trash)
        {
            if (Mathf.Abs(pMan.RB.velocity.x) > 0.1f || Mathf.Abs(pMan.RB.velocity.y) > 0.1f) VisBox.enabled = true;
            else VisBox.enabled = false;
        }


        if (startedJumping)
        {
            GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * transform.localScale.y), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            startedJumping = false;
            return;
        }

        if (justLanded)
        {
            GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * transform.localScale.y), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            justLanded = false;
            return;
        }

        if (dash)
        {
            anim.SetTrigger("Dash");
            dash = false;
        }

        if (Input.GetMouseButtonDown(0) && !attacking && canMove && !trash)
        {
            canMove = false;
            attacking = true;
            anim.Play("Player_CQC1");
        }
    }

    public void Hide()
    {
        sr.color = Color.gray;
        anim.Play("Player_Hide");
        VisBox.enabled = false;
        DashBox.enabled = false;
        canMove = false;
    }

    public void UnHide()
    {
        sr.color = Color.white;
        anim.SetTrigger("Unhide");
        VisBox.enabled = true;
        DashBox.enabled = true;
        canMove = true;
    }

    public void Death()
    {
        anim.Play("Player_Death");
        VisBox.enabled = false;
        DashBox.enabled = false;
        canMove = false;
    }
}
