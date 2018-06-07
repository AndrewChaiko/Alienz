using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : Graphic, IPointerDownHandler
{
    public bool Jump { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        Jump = true;
    }

    private void Update()
    {
        Jump = false;
    }
}
