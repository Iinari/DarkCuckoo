using SnIProductions;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public BattleStateStatus currentState;

    public void StartBattle()
    {
        currentState = GetComponent<BattleStateStatus>();
        BattleEvents.BattleStarted();
    }

    //Post-load logic (example for visuals, not data assignment), data should be assigned through IDataPersistence interface 
    public void StartLoadedBattle()
    {
        BattleEvents.BattleLoaded();
    }

    public void StartPlayerTurn()
    {
        currentState.SetState(BattleState.PlayerTurn);

        BattleEvents.TurnStarted();
    }

    public void EndTurn()
    {
        BattleEvents.OnTurnEnded?.Invoke();
    }

    public void EndBattle(BattleResult result)
    {
        
        currentState.SetState(BattleState.Ended);

        BattleEvents.BattleEnded(result);
    }

    public void CardPlayed(Card card)
    {
        BattleEvents.OnCardPlayed?.Invoke(card);
    }

    public void ShuffleDeck()
    {
        BattleEvents.OnDeckShuffled?.Invoke();
    }
}
