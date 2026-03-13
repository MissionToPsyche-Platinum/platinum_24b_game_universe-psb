using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
//Simple class to control translator graphics. No effect on gameplay
public class Translator_lights : MonoBehaviour
{
    public GameObject redLight;
    public GameObject blueLight;
    public GameObject greenLight;
    public GameObject yellowLight;
    public TMP_Text LoadLabel;
    
    public float interval = 1f;
    public float duration = 2f;
    public float dotInterval = 0.3f;

    public TMP_Text CPU1Banter;
    public TMP_Text CPU2Banter;
    public TMP_Text CPU3Banter;
    public TMP_Text CPU4Banter;
    public TMP_Text CPU5Banter;

    private Coroutine flashRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Loading());
        interval = 0.1f;
        
    }

    void Update()
    {
        // SIMPLE IF / ELSE CHAIN LIKE YOU ASKED FOR
        if (!string.IsNullOrEmpty(CPU1Banter.text))
        {
            StartFlashing();
        }
        else if (!string.IsNullOrEmpty(CPU2Banter.text))
        {
            StartFlashing();
        }
        else if (CPU3Banter != null && !string.IsNullOrEmpty(CPU3Banter.text))
        {
            StartFlashing();
        }
        else if (CPU4Banter != null && !string.IsNullOrEmpty(CPU4Banter.text))
        {
            StartFlashing();
        }
        else if (CPU5Banter != null && !string.IsNullOrEmpty(CPU5Banter.text))
        {
            StartFlashing();
        }
        else
        {
            StopFlashing();
        }
    }
    private void StartFlashing()
    {
        if (flashRoutine == null)
            flashRoutine = StartCoroutine(Flash());
    }

    private void StopFlashing()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
        }

        // turn all lights off
        redLight.SetActive(false);
        blueLight.SetActive(false);
        greenLight.SetActive(false);
        yellowLight.SetActive(false);
    }


    private System.Collections.IEnumerator Flash()
    {
        
        while (true)
        {
           
            redLight.SetActive(true);
            blueLight.SetActive(false);
            greenLight.SetActive(false);
            yellowLight.SetActive(true);
            yield return new WaitForSeconds(interval);

            redLight.SetActive(false);
            greenLight.SetActive(true);
            yellowLight.SetActive(false);
            blueLight.SetActive(true);
            yield return new WaitForSeconds(interval);
        }
    }
    private System.Collections.IEnumerator Loading()
    {
        float timer = 0f;
       // int dotCount = 0;

        while (timer < duration)
        {
            LoadLabel.text += ".";   // append to whatever is already there

            yield return new WaitForSeconds(dotInterval);
            timer += dotInterval;

        }

        LoadLabel.text = ""; // clear after 2 seconds
    }


}
