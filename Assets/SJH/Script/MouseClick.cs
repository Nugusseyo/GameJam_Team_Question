using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player playerC;
    void Start()
    {
        playerC = GameManager.Instance.Player;
    }

    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            playerC.DragStart(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            playerC.DragEnd();
        if (eventData.button == PointerEventData.InputButton.Right)
            playerC.ResetDrag();
    }

}
