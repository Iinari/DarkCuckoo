using UnityEngine;
using SnIProductions;
using System;

public class UnitHealthState : MonoBehaviour
{
    public HealthState CurrentState {  get; private set; } = HealthState.Full;

    public event Action<HealthState> OnStateChanged;

    public void SetState(HealthState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;

        OnStateChanged?.Invoke(CurrentState);
    }

}
