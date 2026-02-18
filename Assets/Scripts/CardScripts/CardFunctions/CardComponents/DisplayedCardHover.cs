using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayedCardHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private CardInteractionState state;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (state.CurrentState == CardState.Default)
        {
            state.SetState(CardState.Hovered);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (state.CurrentState == CardState.Hovered)
        {
            state.ResetToDefault();
        }
    }
}
