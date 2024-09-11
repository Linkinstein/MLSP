using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    LevelManager lm;

    public static PlayerHealth instance;
    public static PlayerAnimator pAnim;
    [SerializeField] public int health = 10;
    [SerializeField] public int maxHealth = 10;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lm = LevelManager.instance;
        pAnim = PlayerAnimator.instance;
    }

    private void Update()
    {
        if (health <= 0)
        {
            pAnim.Death();
        }
    }

    public void TakeDamage(int dmg)
    {
        PlayerAnimator.instance.UnHide();
        health -= dmg;
        lm.hitTaken++;
    }
}
