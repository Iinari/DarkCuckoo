using UnityEngine;
using SnIProductions;
using System;

public class BattleStateStatus : MonoBehaviour
{
    public BattleState CurrentState {  get; private set; } = BattleState.Start;

    public event Action<BattleState> OnStateChanged;

    public void SetState(BattleState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        OnStateChanged?.Invoke(CurrentState);
    }
}
