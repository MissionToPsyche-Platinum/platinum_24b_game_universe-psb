using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// supports screen space -camera
/// </summary>
public class TooltipTrig_Camera : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipCamera.Instance.Show(message, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipCamera.Instance.Hide();
    }

}
