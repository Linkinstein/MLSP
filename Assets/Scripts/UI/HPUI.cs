using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    private Image img;
    private PlayerHealth pH;

    void Start()
    {
        img = GetComponent<Image>();
        pH = PlayerHealth.instance;
    }

    void Update()
    {
        img.fillAmount = (float)pH.health/(float)pH.maxHealth;
    }
}
