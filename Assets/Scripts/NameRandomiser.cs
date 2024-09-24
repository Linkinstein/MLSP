using TMPro;
using UnityEngine;

public class NameRandomiser : MonoBehaviour
{
    TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        RandomizeName();
    }

    string[] m = { "Metal", "Magical", "Mongoose", "Metropolitan", "Mackerel", "Midnight", "Moonlight", "Midday", "Military", "Mechanized" };
    string[] l = { "Lockdown", "Love", "Lucky", "Lonely", "Ligma", "Latrine", "Legion", "Liberation", "Lethal", "Long" };
    string[] s = { "Special", "Silent", "Strategic", "Secret", "Stealth", "Shadow", "Sonic", "Support", "Security", "Short" };
    string[] p = { "Project", "Panties", "Procedure", "Patrol", "Protocol", "Program", "Pursuit", "Plan", "People", "Potato" };

    public void RandomizeName()
    {
        string randomM = m[Random.Range(0, m.Length)];
        string randomL = l[Random.Range(0, l.Length)];
        string randomS = s[Random.Range(0, s.Length)];
        string randomP = p[Random.Range(0, p.Length)];

        string randomName =  $"{randomM} {randomL} {randomS} {randomP}";
        text.SetText(randomName);
    }
}