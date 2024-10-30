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
    [SerializeField] private float crossfadeDuration = 3.0f;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource AM;
    private float BGMCap = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timer = 0.01f;
        BGMCap = BGM.volume;
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
        else if (alerted)
        {
            StartCoroutine(ConvertToNormal());
            alerted = false;
        }
    }

    private void ConvertToAlert()
    {
        StopAllCoroutines();
        BGM.volume = 0;
        if (AM.isPlaying) AM.Stop();
        AM.volume = BGMCap;
        AM.Play();
    }

    IEnumerator ConvertToNormal()
    {
        float elapsedTime = 0f;
        float initialAMVolume = AM.volume;
        float initialBGMVolume = BGM.volume;

        while (elapsedTime < crossfadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / crossfadeDuration;

            // Linearly interpolate both volumes
            AM.volume = Mathf.Lerp(initialAMVolume, 0f, t);
            BGM.volume = Mathf.Lerp(initialBGMVolume, BGMCap, t);

            yield return null;
        }

        AM.volume = 0f;
        AM.Stop();
        BGM.volume = BGMCap;
    }

    public void Alert()
    {
        if (!alerted)
        {
            panelColor = Color.red;
            PanelOn();
            text.SetText("Intruder Detected");
            if (!AS.isPlaying) AS.Play();
            ConvertToAlert();
        }
        alerted = true;
        timer = panelLife;
    }

    public void Sustivity()
    {
        if (!alerted)
        {
            timer = panelLife;
            panelColor = Color.yellow;
            PanelOn();
            text.SetText("Suspicion Raised");
            if (!AS.isPlaying) AS.Play();
        }
    }

    public void BodyFound()
    {
        if (!alerted)
        {
            timer = panelLife;
            panelColor = Color.yellow;
            PanelOn();
            text.SetText("Body found");
            if (!AS.isPlaying) AS.Play();
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
