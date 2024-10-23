using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private UIManager uiMan;

    [SerializeField] AudioSource DeadAS;

    public bool canMove = true;
    public bool seen = false;

    public bool attacking = false;
    public bool isDashing = false;

    public BoxCollider2D VisBox;
    public BoxCollider2D DashBox;

    public bool canTrash = false;
    public bool trash = false;

    public bool dead = false;

    public int hpS = 0;
    public int hpB = 0;


    public float hpRT = 0;
    [SerializeField] private int _health = 10;
    public int health
    {
        get { return _health; } 
        set 
        {
            if (value < _health) hpRT = 1;
            _health = value;
        } 
    }

    [SerializeField] public int maxHealth = 10;

    public float spRT = 0;
    [SerializeField] private int _stamina = 100;
    public int stamina
    {
        get { return _stamina; }
        set
        {
            if (value < _stamina) spRT = 1;
            _stamina = value;
        }
    }

    [SerializeField] public int maxStamina = 100;
    [SerializeField] public int staminaRegen = 10;

    public Rigidbody2D RB { get; private set; }

    private void Awake()
    {
        Instance = this;
        RB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        uiMan = UIManager.Instance;
    }

    private void Update()
    {
        if (hpRT > 0) hpRT -= Time.deltaTime;
        if (spRT > 0) spRT -= Time.deltaTime;
    }

    public void Death()
    {
        dead = true;
        maxHealth = 0;
        maxStamina = 0;
        DeadAS.Play();
        uiMan.Dead();
    }
}
