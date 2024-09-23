using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PASystem : MonoBehaviour
{
    public static PASystem Instance;

    [SerializeField] Image header;
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] Image panel;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Color panelColor = Color.white;
    [SerializeField] Color headerColor = Color.black;
    [SerializeField] float panelLife = 6;
    [SerializeField] float timer = 0;
    [SerializeField] bool alerted = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timer = 0.01f;
    }

    void Update()
    {
        if (timer > 0) 
        {
            timer -= Time.deltaTime;
            if (timer < 2)
            {
                float halpha = Mathf.Lerp(0f, 0.75f, timer / (panelLife - 4));
                float alpha = Mathf.Lerp(0f, 1f, timer / (panelLife - 4));
                panel.color = new Vector4(panelColor.r, panelColor.g, panelColor.b, halpha);
                text.alpha = alpha;
                header.color = new Vector4(headerColor.r, headerColor.g, headerColor.b, alpha);
                headerText.alpha = alpha;
            }
        }
        else alerted = false;
    }

    public void Alert()
    {
        PanelOn();
        timer = panelLife;
        panelColor = Color.red;
        text.SetText("Intruder Detected");
    }

    public void Sustivity()
    {
        if (!alerted)
        {
            PanelOn();
            timer = panelLife;
            panelColor = Color.yellow;
            text.SetText("Sussiness Detected");
        }
    }

    public void BodyFound()
    {
        if (!alerted)
        {
            PanelOn();
            timer = panelLife;
            panelColor = Color.yellow;
            text.SetText("Body found");
        }
    }

    public void PanelOn()
    {
        panel.color = new Vector4(panelColor.r, panelColor.g, panelColor.b, 0.75f);
        text.alpha = 1;
        header.color = new Vector4(headerColor.r, headerColor.g, headerColor.b, 1);
        headerText.alpha = 1;
    }
}
