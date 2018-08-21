using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : Graphic, IPointerDownHandler, IPointerUpHandler
{
    public bool IsDown { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDown = false;
    }
}
