using System.Collections;
using TMPro;
using UnityEngine;

/// 
/// Displays a banter line on a TMP_Text for a brief duration.
/// Compatible with TurnDriver.cs and CPU strategy tests.
/// 
public class BanterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private float defaultDurationSec = 3f;

    // display method
    public void Display(string text, float durationSec = -1f)
    {
        if (label == null) return;
        label.text = text ?? string.Empty;
        StopAllCoroutines();
        StartCoroutine(ClearAfter(durationSec > 0f ? durationSec : defaultDurationSec));
    }

    //  helper
    public void ShowLine(string text)
    {
        // reuse Display() with default timing
        Display(text, defaultDurationSec);
        Debug.Log(text); // also log to console for easy testing
    }

    private IEnumerator ClearAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (label != null) label.text = string.Empty;
    }
}
