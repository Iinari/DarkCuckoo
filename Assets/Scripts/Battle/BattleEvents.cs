using System;
using SnIProductions;

public static class BattleEvents
{
    //Flow (ONLY BattleManager fires)
    public static Action OnBattleStarted;
    public static Action OnTurnStarted;
    public static Action OnTurnEnded;
    public static Action<BattleResult> OnBattleEnded;

    //Gameplay (fired by systems)
    public static Action<Card> OnCardPlayed;
    public static Action OnDeckShuffled;
    public static Action OnHeroSet;

    //Loading
    public static Action OnBattleLoaded;


    public static void BattleStarted()
    {
        OnBattleStarted.Invoke();
    }

    public static void TurnStarted()
    {
        OnTurnStarted.Invoke();
    }

    public static void BattleLoaded()
    {
        OnBattleLoaded.Invoke();
    }

    public static void BattleEnded(BattleResult result)
    {
        OnBattleEnded.Invoke(result);
    }
}