using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public TMP_Text text;

    public void AddMessage(string msg)
    {
        Debug.Log("AddMessage CALLED");

        text.text += "\n" + msg;
        StartCoroutine(ResizeAndScroll());
    }

    private IEnumerator ResizeAndScroll()
    {
        // Wait 1 frame so TMP updates its geometry
        yield return null;
        Debug.Log("Scrolling object: " + scrollRect.gameObject.name);

        // Now preferredHeight is correct
        float newHeight = text.preferredHeight;
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }





}
