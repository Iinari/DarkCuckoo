using SnIProductions;
using UnityEngine;

public class BattleEventManager : MonoBehaviour
{
    public BattleStateStatus currentState;

    public void StartBattle()
    {
        currentState.SetState(BattleState.Start);

        GameEvents.OnBattleStarted?.Invoke();
    }

    public void StartPlayerTurn()
    {
        currentState.SetState(BattleState.PlayerTurn);

        GameEvents.OnTurnStarted?.Invoke();
    }

    public void EndTurn()
    {
        GameEvents.OnTurnEnded?.Invoke();
    }

    public void EndBattle(BattleResult result)
    {
        currentState.SetState(BattleState.Ended);

        GameEvents.OnBattleEnded?.Invoke(result);
    }

    public void CardPlayed(Card card)
    {
        GameEvents.OnCardPlayed?.Invoke(card);
    }

    public void ShuffleDeck()
    {
        GameEvents.OnDeckShuffled?.Invoke();
    }
}
