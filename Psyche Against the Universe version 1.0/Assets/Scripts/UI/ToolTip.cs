using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public static ToolTip Instance;

    public RectTransform ttbackground;
    public TMP_Text tooltipText;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(string message, Vector2 position)
    {
        tooltipText.text = message;
        gameObject.SetActive(true);

        // Offset so tooltip doesn't spawn under the cursor
        Vector2 offset = new Vector2(20f, -20f);
        transform.position = position + offset;

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
