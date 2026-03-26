using TMPro;
using UnityEngine;

/// <summary>
/// Runs tool tips but conforms to the screen space camera format.
/// </summary>
public class ToolTipCamera : MonoBehaviour
{
    public static ToolTipCamera Instance;

    public RectTransform ttbackground;
    public TMP_Text tooltipText;

    Canvas _canvas;
    RectTransform _canvasRect;

    void Awake()
    {
        Instance = this;
        _canvas = GetComponentInParent<Canvas>();
        _canvasRect = _canvas.transform as RectTransform;
        gameObject.SetActive(false);
    }

    public void Show(string message, Vector2 screenPosition)
    {
        tooltipText.text = message;
        gameObject.SetActive(true);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(

            _canvasRect,          // reference rect (canvas)
            screenPosition,       // mouse position in screen space
            _canvas.worldCamera,  // IMPORTANT: pass the canvas camera in Screen Space - Camera
            out localPoint
        );

        // Now place the tooltip in canvas-local space
        Vector2 offset = new Vector2(20f, -20f);
        (transform as RectTransform).anchoredPosition = localPoint + offset;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
