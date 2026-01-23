using UnityEngine;
using SnIProductions;
using System;

public class CardInteractionState : MonoBehaviour
{
    public CardState CurrentState { get; private set; } = CardState.Default;

    public event Action<CardState> OnStateChanged;

    public void SetState(CardState newState)
    {
        
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        //Debug.Log(newState);
        OnStateChanged?.Invoke(CurrentState);
    }

    public void ResetToDefault()
    {
        SetState(CardState.Default);
    }
}