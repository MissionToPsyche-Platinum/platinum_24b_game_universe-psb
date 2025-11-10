using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays a banter line on a TMP_Text for a brief duration.
/// still need to drag it and add to scene, let me know if i am doing this right lol
/// </summary>
public class BanterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private float defaultDurationSec = 3f;

    public void Display(string text, float durationSec = -1f)
    {
        if (label == null) return;
        label.text = text ?? string.Empty;
        StopAllCoroutines();
        StartCoroutine(ClearAfter(durationSec > 0f ? durationSec : defaultDurationSec));
    }

    private IEnumerator ClearAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (label != null) label.text = string.Empty;
    }
}
