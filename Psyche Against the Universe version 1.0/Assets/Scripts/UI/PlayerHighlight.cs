using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class PlayerHighlight : MonoBehaviour
{
    private Outline outline;
    private Coroutine flashRoutine;

    void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    public void HighlightOn()
    {
        if (outline != null)
            outline.enabled = true;
    }

    public void HighlightOff()
    {
        if (outline != null)
            outline.enabled = false;
    }

    public void StartFlashing(float interval = 1f)
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine(interval));
    }

    public void StopFlashing()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = null;
        HighlightOff();
    }

    private IEnumerator FlashRoutine(float interval)
    {
        while (true)
        {
            outline.enabled = !outline.enabled;
            yield return new WaitForSeconds(interval);
        }
    }

}
