using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    UIManager UIMan;

    [SerializeField] GameObject AllAreas;

    public static LevelManager instance;
    public GameObject PlayerArea;
    public bool highAlert = false;

    public int alerted = 0;
    public int takedown = 0;
    public int hitTaken = 0;
    public float timer = 0;

    public int reinforcementNo;

    public int switches = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIMan = UIManager.Instance;    

    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void End()
    {
        float t = timer;

        int minutes = Mathf.FloorToInt(t / 60);
        int seconds = Mathf.FloorToInt(t % 60);

        string time = string.Format("{0:D2}:{1:D2}", minutes, seconds);


        int speedScore = 5000-((int)t * -1);

        int alertedScore = alerted * -200;

        int hitTakenScore = hitTaken * -50;

        int takeDownScore = takedown * 300;

        bool nokills = takedown <= 0;

        bool noalerts = alerted <= 0;

        bool perfect = nokills && noalerts;

        int totalScore = speedScore + alertedScore + hitTakenScore+ takeDownScore;

        if(nokills) totalScore += 10000;
        if (noalerts) totalScore += 75000;
        if (perfect) totalScore += 100000;

        UIMan.End(time, speedScore, alertedScore, hitTakenScore, takeDownScore, nokills, noalerts, perfect, totalScore);
        AllAreas.SetActive(false);
    }
}
