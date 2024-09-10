using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator instance;
    private UIManager UIMan;

    private PlayerInput mov;
    public Animator anim;
    private SpriteRenderer sr;
    public bool attacking = false;
    public GameObject[] CQCBox;
    public BoxCollider2D VisBox;
    public BoxCollider2D DashBox;
    public bool canMove
    {
        get {return mov.canMove;}
        set { mov.canMove = value; }
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
        mov = GetComponent<PlayerInput>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = sr.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckAnimationState();
    }

    private void CheckAnimationState()
    {
        anim.SetFloat("Vel Y", mov.RB.velocity.y);
        anim.SetBool("Walking", Mathf.Abs(mov.RB.velocity.x) > 0.1f);

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

        if (Input.GetMouseButtonDown(0) && !attacking && canMove && !UIMan.pause)
        {
            canMove = false;
            attacking = true;
            anim.Play("Player_CQC1");
        }
    }

    public void Lunge()
    { 
        mov.Lunge();
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
