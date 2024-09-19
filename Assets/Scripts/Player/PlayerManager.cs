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

    public Rigidbody2D RB { get; private set; }

    private void Awake()
    {
        Instance = this;
        RB = GetComponent<Rigidbody2D>();
    }

}
