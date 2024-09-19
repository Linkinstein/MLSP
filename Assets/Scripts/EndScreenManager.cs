using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public static EndScreenManager  Instance;

    [SerializeField] private GameObject ESUI;

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI alerts;
    [SerializeField] private TextMeshProUGUI hitsTaken;
    [SerializeField] private TextMeshProUGUI takedowns;
    [SerializeField] private GameObject noKills;
    [SerializeField] private GameObject noAlerts;
    [SerializeField] private GameObject perfectStealth;
    [SerializeField] private TextMeshProUGUI totalScore;

    private void Awake()
    {
        Instance = this;
    }

    public void End(string time, int spd, int alertos, int hiTakis, int takis, bool nills, bool nalerts, bool perfsta, int totesco)
    {
        ESUI.SetActive(true);

        timer.SetText(time);
        speed.SetText(spd.ToString());
        alerts.SetText(alertos.ToString());
        hitsTaken.SetText(hiTakis.ToString());
        takedowns.SetText(takis.ToString());
        totalScore.SetText(totesco.ToString());

        noKills.SetActive(nills);
        noAlerts.SetActive(nalerts);
        perfectStealth.SetActive(perfsta);
    }
}
