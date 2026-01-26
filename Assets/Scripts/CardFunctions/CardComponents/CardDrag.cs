using SnIProductions;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler
{
    private CardInteractionState state;
    private RectTransform rectTransform;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (state.CurrentState == CardState.Hovered)
        {
            state.SetState(CardState.Dragging);
            rectTransform.localRotation = Quaternion.identity;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (state.CurrentState != CardState.Dragging)
            return;

        rectTransform.position = Input.mousePosition;
    }
}
