using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalsUI : MonoBehaviour
{
    [SerializeField] private Image hpIMG;
    [SerializeField] private Image staIMG;
    private PlayerManager pMan;

    void Start()
    {
        pMan = PlayerManager.Instance;
    }

    void Update()
    {
        hpIMG.fillAmount = (float)pMan.health / (float)pMan.maxHealth;
        staIMG.fillAmount = (float)pMan.stamina / (float)pMan.maxStamina;
    }
}
