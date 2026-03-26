using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTip.Instance.Show(message, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.Hide();
    }

}
