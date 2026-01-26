using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandCardHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private CardInteractionState state;
    private HandManager handManager;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        handManager = FindFirstObjectByType<HandManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (state.CurrentState == CardState.Default)
        {
            state.SetState(CardState.Hovered);
            handManager.UpdateHovered(gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (state.CurrentState == CardState.Hovered)
        {
            state.ResetToDefault();
            handManager.UpdateHandVisuals();
        }
    }
}
