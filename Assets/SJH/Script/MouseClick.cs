using Hwan;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player playerC;
    void Start()
    {
        playerC = GameManager.Instance.Player;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            playerC.DragStart(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (eventData.button == PointerEventData.InputButton.Right)
            TutorialManager.Instance.TryPassTutorial(TutorialType.Cancle);
            playerC.ResetDrag();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            playerC.DragEnd();
    }

}
