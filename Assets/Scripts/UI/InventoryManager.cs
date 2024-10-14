using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private PlayerHealth pH;
    [SerializeField] private TextMeshProUGUI hpSCount;
    [SerializeField] private TextMeshProUGUI hpBCount;

    private void Awake()
    {
        pH = PlayerHealth.Instance;
    }

    private void Update()
    {
        if (pH != null) 
        {
            hpSCount.SetText(pH.hpS + "");
            hpBCount.SetText(pH.hpB + "");
        }
    }

    public void UseHpS()
    {
        if(pH != null) pH.UseHpS();
    }

    public void UseHpB()
    {
        if (pH != null) pH.UseHpB();
    }
}
