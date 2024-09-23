using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public bool canMove = true;
    public bool seen = false;
    public bool attacking = false;
    public bool isDashing = false;
    public bool dead = false;


    [SerializeField] public int health = 10;
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public int stamina = 100;
    [SerializeField] public int maxStamina = 100;
    [SerializeField] public int staminaRegen = 10;

    public Rigidbody2D RB { get; private set; }

    private void Awake()
    {
        Instance = this;
        RB = GetComponent<Rigidbody2D>();
    }

}
