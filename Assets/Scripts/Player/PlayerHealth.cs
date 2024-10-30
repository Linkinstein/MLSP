using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    LevelManager lm;
    PlayerAnimator pAnim;
    PlayerManager pMan;
    [SerializeField] private SpriteRenderer sr;

    public int health
    {
        get { return pMan.health; }
        set { pMan.health = value; }
    }
    public int maxHealth
    {
        get { return pMan.maxHealth; }
        set { pMan.maxHealth = value; }
    }
    public int stamina
    {
        get { return pMan.stamina; }
        set { pMan.stamina = value; }
    }
    public int maxStamina
    {
        get { return pMan.maxStamina; }
        set { pMan.maxStamina = value; }
    }
    public int staminaRegen
    {
        get { return pMan.staminaRegen; }
        set { pMan.staminaRegen = value; }
    }
    public int hpS
    {
        get { return pMan.hpS; }
        set { pMan.hpS = value; }
    }
    public int hpB
    {
        get { return pMan.hpB; }
        set { pMan.hpB = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pMan = PlayerManager.Instance;
        lm = LevelManager.instance;
        pAnim = PlayerAnimator.instance;
        InvokeRepeating("RegenerateStamina", 0f, 1f);
        InvokeRepeating("RegenerateHealth", 0f, 5f);
    }

    private void Update()
    {
        if (health <= 0 && !pMan.dead)
        {
            pMan.dead = true;
            pAnim.Death();
            pMan.Death();
        }

    }

    public void TakeDamage(int dmg)
    {
        pMan.DashBox.enabled = true;
        pMan.VisBox.enabled = true;
        pMan.trash = false;
        PlayerAnimator.instance.UnHide();
        health -= dmg;
        lm.hitTaken++;
        Time.timeScale = 0f;
        StartCoroutine(HitStop());

    }
    IEnumerator HitStop()
    {
        sr.color = Color.red;
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1f;
        sr.color = Color.white;
    }

    private void RegenerateStamina()
    {
        if (!pMan.dead)
        { 
            if (stamina < maxStamina && pMan.spRT<=0)
            {
                stamina += staminaRegen;
            }

            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }
    }
    private void RegenerateHealth()
    {
        if (!pMan.dead)
        {
            if (health < 3 && pMan.hpRT <= 0)
            {
                health += 1;
            }

            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    public void UseHpS()
    {
        if (hpS > 0)
        { 
            health += 3;
            pMan.hpS--;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void UseHpB()
    {
        if (hpB > 0) 
        { 
            health = maxHealth;
            hpB--;
        }
    }
}
